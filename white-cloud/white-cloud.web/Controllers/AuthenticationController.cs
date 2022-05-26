using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using white_cloud.entities;
using white_cloud.identity;
using white_cloud.web.Models.DTOs.Authentication;
using white_cloud.web.Services;

namespace white_cloud.web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IOidcService _oidcService;
        private readonly UserManager<User> _userManager;
        private readonly IUserClaimsPrincipalFactory<User> _claimsPrincipalFactory;
        private readonly IUrlService _urlService;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IOidcService oidcService,
            UserManager<User> userManager,
            IUserClaimsPrincipalFactory<User> claimsPrincipalFactory,
            IUrlService urlService)
        {
            _logger = logger;

            _oidcService = oidcService;
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _urlService = urlService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity);
                }

                var principal = await _claimsPrincipalFactory.CreateAsync(user);
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);
                return Ok();
            }
            return Unauthorized("Invalid email or password");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("oidc/authUrl/{provider}")]
        public async Task<IActionResult> GetCodeFlowUrl(string provider)
        {
            var url = await _oidcService.GetCodeFlowUrlAsync(provider);
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest();
            }
            return Ok(url);
        }

        [AllowAnonymous]
        [HttpGet("oidc/{provider}")]
        public async Task<IActionResult> OidcCodeVerify(string provider, [FromQuery] string code)
        {
            var principal = await _oidcService.GetPrincipalFromCode(provider, code);
            if (principal == null)
            {
                return NotFound();
            }

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);
            return Ok();
        }
    }
}
