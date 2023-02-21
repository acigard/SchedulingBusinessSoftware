using SchedulingBusinessSoftware.Data.DbContexts;
using SchedulingBusinessSoftware.Entities;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddConfigurationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
