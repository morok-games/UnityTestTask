using _Bludoku.Scripts.Boards;
using System;
using UnityEngine;

namespace _Bludoku.Scripts.Score
{
    public class ScoreMediator : MonoBehaviour
    {
        public event Action<ClearResult, int> OnFigureScored;
        public event Action<int> OnComboCounterChanged;
        public event Action OnBoosterActivated;

        [SerializeField] private ScoreView scoreView;
        [SerializeField] private Board board;
        [SerializeField] private ScoreBoosterView boosterView;
        [SerializeField] private ScoreComboView comboView;
        
        private readonly ScoreBoostSystem _scoreBoostSystem = new();
        private readonly ScoreComboSystem _scoreComboSystem = new();

        private void Awake()
        {
            board.OnFigurePlaced += FigurePlaced;
        }

        private void Start()
        {
            ScoreSystem.LoadScore();
            boosterView.SetBoosterEnabled(ScoreSystem.IsBoosterEnabled);
            _scoreBoostSystem.IsBoosted = ScoreSystem.IsBoosterEnabled;
            scoreView.UpdateScore(false);

            _scoreComboSystem.Restore(ScoreSystem.ComboShape, ScoreSystem.ComboCounter);
            UpdateCombo();
        }

        public void ResetScore()
        {
            _scoreComboSystem.Reset();
            ScoreSystem.SetCombo(ClearShape.None, 0);
            ScoreSystem.ResetScore();
            UpdateView();
        }

        private void FigurePlaced(ClearResult result)
        {
            bool wasBoosted = _scoreBoostSystem.IsBoosted;
            _scoreBoostSystem.FigurePlaced(result.ClearedCount);
            int comboBonus = _scoreComboSystem.FigurePlaced(result);

            boosterView.SetBoosterEnabled(_scoreBoostSystem.IsBoosted);
            ScoreSystem.SetBoosterEnabled(_scoreBoostSystem.IsBoosted);
            ScoreSystem.SetCombo(_scoreComboSystem.Shape, _scoreComboSystem.Counter);

            ScoreSystem.AddSetScore(result.ClearedCount);
            scoreView.UpdateScore();

            UpdateCombo();

            if (comboBonus > 0)
            {
                ScoreSystem.AddScore(comboBonus);
                comboView.PlayBonusFly(comboBonus, () => scoreView.UpdateScore());
            }

            if (!wasBoosted && _scoreBoostSystem.IsBoosted)
            {
                OnBoosterActivated?.Invoke();
            }

            OnFigureScored?.Invoke(result, comboBonus);
        }

        private void UpdateView()
        {
            boosterView.SetBoosterEnabled(false);
            _scoreBoostSystem.IsBoosted = false;
            scoreView.UpdateScore(false);
            UpdateCombo();
        }

        private void UpdateCombo()
        {
            comboView.UpdateBonus(_scoreComboSystem.IsActive, _scoreComboSystem.Shape, _scoreComboSystem.Counter);
            OnComboCounterChanged?.Invoke(_scoreComboSystem.Counter);
        }
    }
}