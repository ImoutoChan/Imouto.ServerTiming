using System.Net;
using System.Threading.Tasks;
using Imouto.ServerTiming.Tests.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Imouto.ServerTiming.Tests
{
    public class ServerTimingServerTests
    {
        private const string ServerTimingHeaderName = "Server-Timing";
        private const string ServerTimingHeaderValue = "DELAY;dur=100;desc=\"Arbitrary delay\"";

        private TestServer PrepareTestServer()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<ServerTimingServerStartup>();

            return new TestServer(webHostBuilder);
        }

        [Fact]
        public async Task Request_ReturnsResponseWithServerTimingHeader()
        {
            using var server = PrepareTestServer();
            using var client = server.CreateClient();

            var response = await client.GetAsync("/");

            Assert.True(response.Headers.TryGetValues(ServerTimingHeaderName, out var serverTimingHeaderValue));
        }

        [Fact]
        public async Task Request_ReturnsResponseWithCorrectServerTimingHeader()
        {
            using var server = PrepareTestServer();
            using var client = server.CreateClient();

            var response = await client.GetAsync("/");

            response.Headers.TryGetValues(ServerTimingHeaderName, out var serverTimingHeaderValues);

            Assert.Collection(serverTimingHeaderValues,
                serverTimingHeaderValue => Assert.Equal(ServerTimingHeaderValue, serverTimingHeaderValue));
        }

        [Fact]
        public async Task Request_AllowsTrailers_ReturnsResponseWithServerTimingTrailer()
        {
            using var server = PrepareTestServer();
            using var client = server.CreateClient();

            client.DefaultRequestVersion = HttpVersion.Version20;
            client.DefaultRequestHeaders.Add("TE", "trailers");

            var response = await client.GetAsync("/");

            Assert.True(response.TrailingHeaders.TryGetValues(ServerTimingHeaderName, out var serverTimingHeaderValue));
        }
    }
}
