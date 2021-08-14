using Imouto.ServerTiming.AspNetCore.Headers;
using Microsoft.AspNetCore.Http;

namespace Imouto.ServerTiming.AspNetCore.Extensions
{
    internal static class HttpResponseHeadersExtensions
    {
        public static void SetServerTimingHeader(this HttpResponse response, ServerTimingHeaderValue serverTiming)
        {
            response.SetResponseHeader(HeaderNames.ServerTiming, serverTiming.ToString());
        }

        public static void SetTimingAllowOriginHeader(this HttpResponse response, TimingAllowOriginHeaderValue headerValue)
        {
            response.SetResponseHeader(HeaderNames.TimingAllowOrigin, headerValue.ToString());
        }

        public static void SetServerTimingTrailer(this HttpResponse response, ServerTimingHeaderValue serverTiming)
        {
            response.AppendTrailer(HeaderNames.ServerTiming, serverTiming.ToString());
        }

        private static void SetResponseHeader(this HttpResponse response, string headerName, string headerValue)
        {
            if (string.IsNullOrWhiteSpace(headerValue))
                return;

            if (response.Headers.ContainsKey(headerName))
                response.Headers[headerName] = headerValue;
            else
                response.Headers.Append(headerName, headerValue);
        }
    }
}
