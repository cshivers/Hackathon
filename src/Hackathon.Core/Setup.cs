using Hackathon.Core.Blitz;
using Hackathon.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon.Core
{
    /// <summary>
    /// Contains extension methods for configuring the Core services and features upon application startup.
    /// </summary>
    public static class Setup
    {
        /// <summary>
        /// Adds the Core services and classes to the service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the Core services are to be added.</param>
        /// <returns><paramref name="services"/></returns>
        public static IServiceCollection AddCoreDepenencies(this IServiceCollection services)
        {
            return services
                .AddHttpClient()
                .AddScoped<IBlitzService, BlitzService>()
                .AddScoped<IPlayerService, PlayerService>()
                .AddScoped<IMetaService, MetaService>()
                .AddScoped<IBlitzResponseMapper, BlitzResponseMapper>();
        }
    }
}
