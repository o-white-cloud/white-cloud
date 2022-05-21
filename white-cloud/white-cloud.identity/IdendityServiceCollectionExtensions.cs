using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using white_cloud.identity.Entities;

namespace white_cloud.identity
{
    public static class IdendityServiceCollectionExtensions
    {
        public static void AddIdentity(this IServiceCollection services, IConfigurationRoot configuration)
        {

            var migrationAssembly = typeof(User).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("postgres"), npgOptions =>
                {
                    npgOptions.MigrationsAssembly(migrationAssembly);
                });
            });
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/login");
            services.AddTransient<IdentitySeeder>();
        }
    }
}
