using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using white_cloud.entities;

namespace white_cloud.identity
{
    public class OidcService : IOidcService
    {
        private readonly IdpCache _idpCache;
        private readonly OidcSettings _oidcSettings;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<OidcService> _logger;
        private readonly IUserClaimsPrincipalFactory<User> _claimsPrincipalFactory;

        public OidcService(IdpCache idpCache,
            OidcSettings oidcSettings,
            UserManager<User> userManager,
            ILogger<OidcService> logger, 
            IUserClaimsPrincipalFactory<User> claimsPrincipalFactory)
        {
            _idpCache = idpCache;
            _oidcSettings = oidcSettings;
            _userManager = userManager;
            _logger = logger;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        public async Task<string?> GetCodeFlowUrlAsync(string provider)
        {
            if (!_idpCache.ContainsKey(provider))
            {
                _logger.LogError($"OIDC provider '{provider}' not supported.");
                return null;
            }

            var idpInfo = _idpCache[provider];
            var doc = await idpInfo.DiscoveryCache.GetAsync();
            if (doc.IsError)
            {
                _logger.LogError($"Provider {provider} was found but discovery document could not be read");
                return null;
            }
            var ru = new RequestUrl(doc.AuthorizeEndpoint);
            var url = ru.CreateAuthorizeUrl(
                clientId: idpInfo.ClientId,
                responseType: "code",
                redirectUri: _oidcSettings.CallbackUrl,
                scope: "openid email",
                state: provider);
            return url;
        }

        public async Task<ClaimsPrincipal?> GetPrincipalFromCode(string provider, string code)
        {
            if (!_idpCache.ContainsKey(provider))
            {
                _logger.LogError($"OIDC provider '{provider}' not supported.");
            }

            var idpInfo = _idpCache[provider];
            var doc = await idpInfo.DiscoveryCache.GetAsync();
            if (doc.IsError)
            {
                _logger.LogError($"Provider {provider} was found but discovery document could not be read");
                return null;
            }

            var client = new HttpClient();
            var token = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
            {
                Address = doc.TokenEndpoint,
                Code = code,
                ClientId = idpInfo.ClientId,
                ClientSecret = idpInfo.ClientSecret,
                RedirectUri = _oidcSettings.CallbackUrl,
            });
            if (token.IsError)
            {
                throw new Exception(token.Error);
            }

            var user = await GetOrCreateUserFromIdToken(token.IdentityToken);
            if (user == null)
            {
                return null;
            }

            var principal = await _claimsPrincipalFactory.CreateAsync(user);
            return principal;
        }

        private async Task<User?> GetOrCreateUserFromIdToken(string tokenString)
        {
            var handler = new JwtSecurityTokenHandler();
            var identityToken = handler.ReadJwtToken(tokenString);
            if (identityToken == null)
            {
                _logger.LogError("Could not read Identity token");
                return null;
            }
            var emailClaim = identityToken.Claims?.FirstOrDefault(c => c.Type == "email");
            if (emailClaim == null || string.IsNullOrWhiteSpace(emailClaim.Value))
            {
                _logger.LogWarning("Could not find email claim in Identity token");
                return null;
            }

            var user = await _userManager.FindByEmailAsync(emailClaim.Value);
            if (user == null)
            {
                user = new User
                {
                    Email = emailClaim.Value,
                };
                //Id = identityToken.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.NewGuid().ToString(),

                var createResult = await _userManager.CreateAsync(user);

                if (createResult == null || !createResult.Succeeded)
                {
                    _logger.LogError("Could not create user with email {email}: {errors}", emailClaim.Value, string.Join(Environment.NewLine, createResult?.Errors?.Select(e => e.Description) ?? new List<string>()));
                    return null;
                }
            }

            return user;
        }
    }
}
