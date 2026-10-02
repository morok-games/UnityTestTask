using _Bludoku.Scripts.Analytics.Events;
using _Bludoku.Scripts.Blocks;
using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Core;
using _Bludoku.Scripts.Score;
using UnityEngine;

namespace _Bludoku.Scripts.Analytics
{
    public class AnalyticsTracker : MonoBehaviour
    {
        [SerializeField] private FiguresController figuresController;
        [SerializeField] private ScoreMediator scoreMediator;
        [SerializeField] private GameController gameController;

        private AnalyticsService _analytics;

        private void Awake()
        {
            _analytics = new AnalyticsService(new IAnalyticsProvider[]
            {
                new DebugAnalyticsProvider()
            });

            figuresController.OnFigurePlaced += FigurePlaced;
            figuresController.OnFigureReturned += FigureReturned;
            scoreMediator.OnFigureScored += FigureScored;
            scoreMediator.OnBoosterActivated += BoosterActivated;
            gameController.OnSecondChanceUsed += SecondChanceUsed;
        }

        private void FigurePlaced(Figure figure)
        {
            _analytics.Track(new FigurePlacedEvent(figure.ID));
        }

        private void FigureReturned(Figure figure)
        {
            _analytics.Track(new FigureReturnedEvent(figure.ID));
        }

        private void FigureScored(ClearResult result, int comboBonus)
        {
            if (comboBonus > 0)
                _analytics.Track(new ComboBonusReceivedEvent(comboBonus, result.ClearedShapes.ToString()));
        }

        private void BoosterActivated()
        {
            _analytics.Track(new BoosterActivatedEvent());
        }

        private void SecondChanceUsed()
        {
            _analytics.Track(new PowerUpUsedEvent("second_chance"));
        }
    }
}