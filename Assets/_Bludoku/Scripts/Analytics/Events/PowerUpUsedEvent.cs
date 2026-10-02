using System.Collections.Generic;

namespace _Bludoku.Scripts.Analytics.Events
{
    public class PowerUpUsedEvent : IAnalyticsEvent
    {
        public string Name => "power_up_used";
        public IReadOnlyDictionary<string, object> Parameters { get; }

        public PowerUpUsedEvent(string powerUp)
        {
            Parameters = new Dictionary<string, object>
            {
                { "power_up", powerUp }
            };
        }
    }
}