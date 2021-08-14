using System;
using System.Linq;
using System.Threading.Tasks;
using Imouto.ServerTiming.AspNetCore.Extensions;
using Imouto.ServerTiming.AspNetCore.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Imouto.ServerTiming.AspNetCore
{
    /// <summary>
    ///     Middleware providing support for Server Timing API.
    /// </summary>
    public class ServerTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<ServerTimingOptions> _options;

        /// <summary>
        ///     Instantiates a new <see cref="ServerTimingMiddleware" />.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="options">Server Timing options</param>
        public ServerTimingMiddleware(RequestDelegate next, IOptions<ServerTimingOptions> options)
        {
            _next = next;
            _options = options;
        }

        /// <summary>
        ///     Process an individual request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="serverTiming">The instance of <see cref="IServerTiming" /> for current requet.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task Invoke(HttpContext context, IServerTiming serverTiming)
        {
            var timingAllowOrigin = _options.Value.TimingAllowOrigin;

            if (timingAllowOrigin.Any())
                context.Response.SetTimingAllowOriginHeader(new TimingAllowOriginHeaderValue(timingAllowOrigin));

            return HandleServerTimingAsync(context, serverTiming);
        }

        private async Task HandleServerTimingAsync(HttpContext context, IServerTiming serverTiming)
        {
            if (context.Request.AllowsTrailers() && context.Response.SupportsTrailers())
                await HandleServerTimingAsTrailerHeaderAsync(context, serverTiming);
            else
                await HandleServerTimingAsResponseHeaderAsync(context, serverTiming);
        }

        private async Task HandleServerTimingAsTrailerHeaderAsync(HttpContext context, IServerTiming serverTiming)
        {
            var measurement = _options.Value.IncludeTotalMetric ? serverTiming.Measure("total") : null;

            context.Response.DeclareTrailer(HeaderNames.ServerTiming);

            serverTiming.SetServerTimingDeliveryMode(ServerTimingDeliveryMode.Trailer);

            await _next(context);

            measurement?.Dispose();

            context.Response.SetServerTimingTrailer(new ServerTimingHeaderValue(serverTiming.Metrics));
        }

        private Task HandleServerTimingAsResponseHeaderAsync(HttpContext context, IServerTiming serverTiming)
        {
            var measurement = _options.Value.IncludeTotalMetric ? serverTiming.Measure("total") : null;

            context.Response.OnStarting(() =>
            {

                if (serverTiming.Metrics.Count > 0)
                    context.Response.SetServerTimingHeader(new ServerTimingHeaderValue(serverTiming.Metrics));

                return Task.CompletedTask;
            });

            serverTiming.SetServerTimingDeliveryMode(ServerTimingDeliveryMode.ResponseHeader);

            return _next(context);
        }
    }
}
