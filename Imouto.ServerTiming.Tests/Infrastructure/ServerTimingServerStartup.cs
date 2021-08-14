using System.Threading.Tasks;
using Imouto.ServerTiming.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Imouto.ServerTiming.Tests.Infrastructure
{
    internal class ServerTimingServerStartup
    {
        private const string DelayServerTimingMetricName = "DELAY";
        private const int DelayServerTimingMetricValue = 100;
        private const string DelayServerTimingMetricDescription = "Arbitrary delay";

        private const string ResponseBody = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title>Imouto.ServerTiming.Tests</title></head><body>ServerTimingMiddleware Integration Tests</body></html>";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServerTiming();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseServerTiming()
                .Run(async context =>
                {
                    var serverTiming = context.RequestServices.GetRequiredService<IServerTiming>();

                    await Task.Delay(DelayServerTimingMetricValue);

                    serverTiming.AddMetric(
                        DelayServerTimingMetricName,
                        DelayServerTimingMetricValue,
                        DelayServerTimingMetricDescription);

                    await context.Response.WriteAsync(ResponseBody);
                });
        }
    }
}
