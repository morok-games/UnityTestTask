using System.Collections.Generic;

namespace _Bludoku.Scripts.Analytics.Events
{
    public class FigurePlacedEvent : IAnalyticsEvent
    {
        public string Name => "figure_placed";
        public IReadOnlyDictionary<string, object> Parameters { get; }

        public FigurePlacedEvent(int figureId)
        {
            Parameters = new Dictionary<string, object>
            {
                { "figure_id", figureId }
            };
        }
    }
}