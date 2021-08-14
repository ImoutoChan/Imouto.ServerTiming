using Microsoft.AspNetCore.Builder;

namespace Imouto.ServerTiming.AspNetCore
{
    /// <summary>
    ///     The <see cref="IApplicationBuilder" /> extensions for adding Server Timing API middleware to pipeline.
    /// </summary>
    public static class ServerTimingMiddlewareExtensions
    {
        /// <summary>
        ///     Adds a <see cref="ServerTimingMiddleware" /> to application pipeline.
        /// </summary>
        /// <remarks>
        ///     Place it first on the pipeline
        /// </remarks>
        /// <param name="app">The <see cref="IApplicationBuilder" /> passed to Configure method.</param>
        /// <returns>The original app parameter</returns>
        public static IApplicationBuilder UseServerTiming(this IApplicationBuilder app)
            => app.UseMiddleware<ServerTimingMiddleware>();
    }
}
