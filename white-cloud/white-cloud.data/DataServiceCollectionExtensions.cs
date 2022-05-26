using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using white_cloud.data.EF;
using white_cloud.data.Tests;
using white_cloud.data.Therapists;
using white_cloud.interfaces.Data;

namespace white_cloud.data
{
    public static class DataServiceCollectionExtensions
    {
        public static void AddDataLayer(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddTransient<ITestsRepository, FilesTestsRepository>();
            services.AddTransient<ITestSubmissionsRepository, TestSubmissionsRepository>();
            services.AddTransient<ITherapistsRepository, TherapistsRepository>();

            var migrationAssembly = typeof(WCDbContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<WCDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("postgres"), npgOptions =>
                {
                    npgOptions.MigrationsAssembly(migrationAssembly);
                });
            });
        }
    }
}
