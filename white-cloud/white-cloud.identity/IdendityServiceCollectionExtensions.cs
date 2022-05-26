using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using white_cloud.data.EF;
using white_cloud.entities;
using white_cloud.identity.Tokens;

namespace white_cloud.identity
{
    public static class IdendityServiceCollectionExtensions
    {
        public static void AddIdentity(this IServiceCollection services, IConfigurationRoot configuration)
        {

            var migrationAssembly = typeof(User).GetTypeInfo().Assembly.GetName().Name;
            
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<WCDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<InviteUserTokenProvider>(InviteUserTokenProvider.TokenType);
            services.AddScoped<IUserClaimsPrincipalFactory<User>, ExtendedUserClaimsPrincipalFactory>();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/login");
            services.AddTransient<IdentitySeeder>();
        }
    }
}
