using System;
using System.Collections.Generic;

namespace Imouto.ServerTiming.AspNetCore
{
    /// <summary>
    /// Service Timing Options.
    /// </summary>
    public class ServerTimingOptions
    {
        /// <summary>
        ///     Sets Timing-Allow-Origin header with provided values.
        /// </summary>
        public IReadOnlyCollection<string> TimingAllowOrigin { get; set; } = Array.Empty<string>();

        /// <summary>
        ///     Creates the default metric "total".
        /// </summary>
        public bool IncludeTotalMetric { get; set; } = true;
    }
}
