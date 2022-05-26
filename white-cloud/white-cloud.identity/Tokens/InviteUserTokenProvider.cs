using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using white_cloud.entities;

namespace white_cloud.identity.Tokens
{
    public class InviteUserTokenProvider : DataProtectorTokenProvider<User>
    {
        public const string TokenType = "InviteUser";

        public InviteUserTokenProvider(ILogger<InviteUserTokenProvider> logger, IDataProtectionProvider dataProtectionProvider, IOptions<InviteUserTokenProviderOptions> options) : base(dataProtectionProvider, options, logger)
        {
        }
    }

    public class InviteUserTokenProviderOptions : DataProtectionTokenProviderOptions
    {
    }
}
