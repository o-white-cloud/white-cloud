using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace white_cloud.identity
{
    public static class OidcServiceCollectionExtensions
    {
        public static void AddOidcIdentityProvidersInfo(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var idpSections = configuration.GetSection("OpenIdConnect:IdentityProviders").GetChildren();
            foreach (var idpSection in idpSections)
            {
                var clientId = idpSection["ClientId"];
                var clientSecret = idpSection["ClientSecret"];
                var discoveryUrl = idpSection["DiscoveryUrl"];

                services.AddSingleton(r =>
                {
                    var factory = r.GetRequiredService<IHttpClientFactory>();
                    var cache = new DiscoveryCache(discoveryUrl, () => factory.CreateClient(), new DiscoveryPolicy
                    {
                        ValidateEndpoints = false
                    });
                    return new IdpInfo(idpSection.Key, clientId, clientSecret, cache);
                });
            }
            services.AddSingleton(r =>
            {
                var idps = r.GetServices<IdpInfo>();
                return new IdpCache(idps);
            });
            services.AddSingleton(configuration.GetSection("OpenIdConnect").Get<OidcSettings>());
            services.AddTransient<IOidcService,OidcService>();
        }
    }
}
