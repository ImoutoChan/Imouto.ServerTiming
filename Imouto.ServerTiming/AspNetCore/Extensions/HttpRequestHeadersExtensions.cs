using System.Linq;
using Imouto.ServerTiming.AspNetCore.Headers;
using Microsoft.AspNetCore.Http;

namespace Imouto.ServerTiming.AspNetCore.Extensions
{
    internal static class HttpRequestHeadersExtensions
    {
        private const string AcceptTrailers = "trailers";

        public static bool AllowsTrailers(this HttpRequest request)
        {
            return request.Headers.ContainsKey(HeaderNames.AcceptTransferEncoding)
                   && request.Headers[HeaderNames.AcceptTransferEncoding].Contains(AcceptTrailers);
        }
    }
}
