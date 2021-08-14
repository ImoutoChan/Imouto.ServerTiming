using System;
using System.Collections.Generic;

namespace Imouto.ServerTiming.AspNetCore.Headers
{
    /// <summary>
    ///     Represents value of Timing-Allow-Origin header.
    /// </summary>
    public class TimingAllowOriginHeaderValue
    {
        /// <summary>
        ///     Instantiates a new <see cref="TimingAllowOriginHeaderValue" />.
        /// </summary>
        /// <param name="origins">The collection of origins that are allowed to see values from timing APIs.</param>
        public TimingAllowOriginHeaderValue(IReadOnlyCollection<string> origins) => Origins = origins;

        /// <summary>
        ///     Instantiates a new <see cref="TimingAllowOriginHeaderValue" />.
        /// </summary>
        public TimingAllowOriginHeaderValue() => Origins = Array.Empty<string>();

        /// <summary>
        ///     Gets the collection of origins that are allowed to see values from timing APIs.
        /// </summary>
        public IReadOnlyCollection<string> Origins { get; }

        /// <summary>
        ///     Gets the string representation of header value.
        /// </summary>
        /// <returns>The string representation of header value.</returns>
        public override string ToString() => string.Join(",", Origins);
    }
}
