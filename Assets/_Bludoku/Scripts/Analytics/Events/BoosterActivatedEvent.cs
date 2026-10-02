using System.Collections.Generic;

namespace _Bludoku.Scripts.Analytics.Events
{
    public class BoosterActivatedEvent : IAnalyticsEvent
    {
        public string Name => "booster_activated";
        public IReadOnlyDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
    }
}