using System.Security.Claims;

namespace white_cloud.identity
{
    public interface IOidcService
    {
        Task<string?> GetCodeFlowUrlAsync(string provider);
        Task<ClaimsPrincipal?> GetPrincipalFromCode(string provider, string code);
    }
}
