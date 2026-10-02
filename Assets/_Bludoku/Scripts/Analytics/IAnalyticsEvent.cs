using System.Collections.Generic;

namespace _Bludoku.Scripts.Analytics
{
    public interface IAnalyticsEvent
    {
        string Name { get; }
        IReadOnlyDictionary<string, object> Parameters { get; }
    }
}