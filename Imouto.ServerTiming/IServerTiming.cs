using System.Collections.Generic;

namespace Imouto.ServerTiming
{
    /// <summary>
    ///     Provides support for Server Timing API.
    /// </summary>
    public interface IServerTiming
    {
        /// <summary>
        ///     Gets the metrics delivery mode for current request.
        /// </summary>
        ServerTimingDeliveryMode DeliveryMode { get; }

        /// <summary>
        ///     Gets the collection of metrics for current request.
        /// </summary>
        IReadOnlyCollection<ServerTimingMetric> Metrics { get; }

        /// <summary>
        ///     Adds new metric to request scope
        /// </summary>
        void AddMetric(string metricName, long value, string? description = null);
    }
}
