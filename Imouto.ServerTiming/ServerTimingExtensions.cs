using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Imouto.ServerTiming
{
    /// <summary>
    ///     Utilities for easier logging of server timing performance metric.
    /// </summary>
    public static class ServerTimingExtensions
    {
        /// <summary>
        ///     Measure the time for the scope of returned disposible.
        /// </summary>
        /// <param name="serverTiming">The <see cref="IServerTiming" /> to add metric to.</param>
        /// <param name="metricName">Optional, metric name, if passed will override any caller name.</param>
        public static IDisposable Measure(this IServerTiming serverTiming, string metricName)
            => new ServerTimingInstance(serverTiming, metricName);

        /// <summary>
        ///     Time an async task.
        /// </summary>
        /// <typeparam name="T">Type of the task.</typeparam>
        /// <param name="task">The <see cref="Task" /> to time.</param>
        /// <param name="serverTiming">The <see cref="IServerTiming" /> to add metric to.</param>
        /// <param name="metricName">Optional, metric name, if passed will override any caller name.</param>
        /// <returns></returns>
        public static async Task<T> MeasureTask<T>(
            this IServerTiming serverTiming,
            Task<T> task,
            string metricName)
        {
            using (serverTiming.Measure(metricName))
                return await task;
        }

        internal static void SetServerTimingDeliveryMode(
            this IServerTiming serverTiming,
            ServerTimingDeliveryMode deliveryMode)
        {
            if (serverTiming is ServerTiming concreteServerTiming)
                concreteServerTiming.DeliveryMode = deliveryMode;
        }

        private sealed class ServerTimingInstance : IDisposable
        {
            private readonly string _metricName;
            private readonly IServerTiming _serverTiming;
            private readonly Stopwatch _watch;

            public ServerTimingInstance(IServerTiming serverTiming, string metricName)
            {
                _serverTiming = serverTiming;
                _metricName = metricName;

                _watch = new Stopwatch();
                _watch.Start();
            }

            public void Dispose()
            {
                _watch.Stop();

                _serverTiming.AddMetric(_metricName, _watch.ElapsedMilliseconds, _metricName);
            }
        }
    }
}
