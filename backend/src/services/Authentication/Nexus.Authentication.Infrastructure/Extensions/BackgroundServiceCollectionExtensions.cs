using Microsoft.Extensions.DependencyInjection;
using Nexus.Authentication.Infrastructure.BackgroundServices;

namespace Nexus.Authentication.Infrastructure.Extensions
{
    public static class BackgroundServiceCollectionExtensions
    {
        public static IServiceCollection RegisterBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<TokenCleanupBackgroundService>();

            return services;
        }
    }
}