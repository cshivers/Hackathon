using Hackathon.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
                => services
                    .AddHttpClient()
                    .AddScoped<BlitzService>()
                    .AddScoped<PlayerService>()
                    .AddScoped<MetaService>();
    }
}
