using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Imouto.ServerTiming
{
    internal class ServerTiming : IServerTiming
    {
        // Regex to find chars that are invalid in https://httpwg.org/specs/rfc7230.html#rfc.section.3.2.6
        private static readonly Regex InvalidTokenChars = new("[^&#\\$%&'\\*\\+\\-\\.\\^`\\|~\\w]");

        private readonly ConcurrentDictionary<string, ServerTimingMetric> _metrics = new();

        public ServerTimingDeliveryMode DeliveryMode { get; internal set; } = ServerTimingDeliveryMode.Unknown;

        public IReadOnlyCollection<ServerTimingMetric> Metrics => _metrics.Values.ToList();

        public void AddMetric(string name, long value, string? description = null)
        {
            name = InvalidTokenChars.Replace(name.Replace(' ', '-'), "");

            var wasAdded = _metrics.TryAdd(name, new ServerTimingMetric(name, value, description));

            var counter = 1;
            while (!wasAdded)
            {
                var newName = name + "-" + counter++;
                wasAdded = _metrics.TryAdd(name, new ServerTimingMetric(newName, value, description));
            }
        }
    }
}
