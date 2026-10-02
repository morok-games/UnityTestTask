using System.Collections.Generic;

namespace _Bludoku.Scripts.Analytics.Events
{
    public class ComboBonusReceivedEvent : IAnalyticsEvent
    {
        public string Name => "combo_bonus_received";
        public IReadOnlyDictionary<string, object> Parameters { get; }

        public ComboBonusReceivedEvent(int bonus, string shape)
        {
            Parameters = new Dictionary<string, object>
            {
                { "bonus", bonus },
                { "shape", shape }
            };
        }
    }
}
