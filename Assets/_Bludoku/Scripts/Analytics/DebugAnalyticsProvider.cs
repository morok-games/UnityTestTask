using System.Linq;
using UnityEngine;

namespace _Bludoku.Scripts.Analytics
{
    public class DebugAnalyticsProvider : IAnalyticsProvider
    {
        public void Track(IAnalyticsEvent analyticsEvent)
        {
            string parameters = string.Join(", ", analyticsEvent.Parameters.Select(p => $"{p.Key}={p.Value}"));

            Debug.Log($"[Analytics] {analyticsEvent.Name} {{{parameters}}}");
        }
    }
}