using System.Collections.Generic;

namespace Imouto.ServerTiming.AspNetCore.Headers
{
    /// <summary>
    ///     Represents value of Server-Timing header.
    /// </summary>
    public class ServerTimingHeaderValue
    {
        /// <summary>
        ///     Instantiates a new <see cref="ServerTimingHeaderValue" />.
        /// </summary>
        /// <param name="metrics">The collection of metrics.</param>
        public ServerTimingHeaderValue(IReadOnlyCollection<ServerTimingMetric> metrics) => Metrics = metrics;

        /// <summary>
        ///     Gets the collection of metrics.
        /// </summary>
        public IReadOnlyCollection<ServerTimingMetric> Metrics { get; }

        /// <summary>
        ///     Gets the string representation of header value.
        /// </summary>
        /// <returns>The string representation of header value.</returns>
        public override string ToString() => string.Join(",", Metrics);
    }
}
