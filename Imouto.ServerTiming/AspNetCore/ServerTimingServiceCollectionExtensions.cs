using System;
using Microsoft.Extensions.DependencyInjection;

namespace Imouto.ServerTiming.AspNetCore
{
    /// <summary>
    ///     The <see cref="IServiceCollection" /> extensions for adding Server Timing API related services.
    /// </summary>
    public static class ServerTimingServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers default service which provides support for Server Timing API.
        /// </summary>
        /// <param name="services">The collection of service descriptors.</param>
        /// <param name="configureOptions">Configure options.</param>
        /// <returns>The collection of service descriptors.</returns>
        public static IServiceCollection AddServerTiming(
            this IServiceCollection services,
            Action<ServerTimingOptions>? configureOptions = null)
        {
            services.AddScoped<IServerTiming, ServerTiming>();

            if (configureOptions != null) services.Configure(configureOptions);

            return services;
        }
    }
}
