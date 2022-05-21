using IdentityModel.Client;

namespace white_cloud.identity
{
    public class IdpInfo
    {
        public IdpInfo(string providerName, string clientId, string clientSecret, DiscoveryCache discoveryCache)
        {
            Name = providerName;
            ClientId = clientId;
            ClientSecret = clientSecret;
            DiscoveryCache = discoveryCache;
        }

        public string Name { get; private set; }
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public DiscoveryCache DiscoveryCache { get; private set; }
    }
}
