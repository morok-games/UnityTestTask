namespace _Bludoku.Scripts.Analytics
{
    public interface IAnalyticsProvider
    {
        void Track(IAnalyticsEvent analyticsEvent);
    }
}
