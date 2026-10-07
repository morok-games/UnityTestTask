using System.Collections.Generic;

namespace _Bludoku.Scripts.Analytics
{
    public class AnalyticsEvent : IAnalyticsEvent
    {
        public string Name { get; }
        public IReadOnlyDictionary<string, object> Parameters { get; }

        private AnalyticsEvent(string name, Dictionary<string, object> parameters = null)
        {
            Name = name;
            Parameters = parameters ?? new Dictionary<string, object>();
        }

        public static AnalyticsEvent FigurePlaced(int figureId)
        {
            return new AnalyticsEvent("figure_placed", new Dictionary<string, object>
            {
                { "figure_id", figureId }
            });
        }

        public static AnalyticsEvent FigureReturned(int figureId)
        {
            return new AnalyticsEvent("figure_returned", new Dictionary<string, object>
            {
                { "figure_id", figureId }
            });
        }

        public static AnalyticsEvent ComboBonusReceived(int bonus, string shape)
        {
            return new AnalyticsEvent("combo_bonus_received", new Dictionary<string, object>
            {
                { "bonus", bonus },
                { "shape", shape }
            });
        }

        public static AnalyticsEvent BoosterActivated()
        {
            return new AnalyticsEvent("booster_activated");
        }

        public static AnalyticsEvent PowerUpUsed(string powerUp)
        {
            return new AnalyticsEvent("power_up_used", new Dictionary<string, object>
            {
                { "power_up", powerUp }
            });
        }
    }
}