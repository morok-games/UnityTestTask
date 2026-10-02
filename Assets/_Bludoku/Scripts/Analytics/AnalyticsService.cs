using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Bludoku.Scripts.Analytics
{
    public class AnalyticsService
    {
        private readonly List<IAnalyticsProvider> _providers;

        public AnalyticsService(IEnumerable<IAnalyticsProvider> providers)
        {
            _providers = new List<IAnalyticsProvider>(providers);
        }

        public void Track(IAnalyticsEvent analyticsEvent)
        {
            foreach (var provider in _providers)
            {
                try
                {
                    provider.Track(analyticsEvent);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}