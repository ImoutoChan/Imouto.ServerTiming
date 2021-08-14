using System;
using System.Globalization;

namespace Imouto.ServerTiming
{
    /// <summary>
    ///     Server timing performance metric.
    /// </summary>
    public struct ServerTimingMetric
    {
        private string? _computedString;

        /// <summary>
        ///     Initializes a new <see cref="ServerTimingMetric" />.
        /// </summary>
        /// <param name="name">The metric name.</param>
        /// <param name="value">The metric value.</param>
        /// <param name="description">The metric description.</param>
        internal ServerTimingMetric(string name, long value, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
            Description = description;

            _computedString = null;
        }

        /// <summary>
        ///     Gets the metric name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the metric value.
        /// </summary>
        public long Value { get; }

        /// <summary>
        ///     Gets the metric description.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        ///     Gets the string representation of metric.
        /// </summary>
        /// <returns>The string representation of metric.</returns>
        public override string ToString()
        {
            var computed = _computedString;

            if (computed is not null)
                return computed;

            computed = Name;

            computed = $"{computed};dur={Value.ToString(CultureInfo.InvariantCulture)}";

            if (!string.IsNullOrEmpty(Description))
                computed = $"{computed};desc=\"{Description}\"";

            _computedString = computed;
            return computed;
        }
    }
}
