using System.Collections.Generic;

namespace _Bludoku.Scripts.Analytics.Events
{
    public class FigureReturnedEvent : IAnalyticsEvent
    {
        public string Name => "figure_returned";
        public IReadOnlyDictionary<string, object> Parameters { get; }

        public FigureReturnedEvent(int figureId)
        {
            Parameters = new Dictionary<string, object>
            {
                { "figure_id", figureId }
            };
        }
    }
}