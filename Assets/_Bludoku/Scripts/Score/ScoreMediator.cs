using _Bludoku.Scripts.Boards;
using System;
using UnityEngine;

namespace _Bludoku.Scripts.Score
{
    public class ScoreMediator : MonoBehaviour
    {
        public event Action<ClearResult, int> OnFigureScored;

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
            comboView.UpdateBonus(_scoreComboSystem.IsActive, _scoreComboSystem.Shape, _scoreComboSystem.Counter);
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
            _scoreBoostSystem.FigurePlaced(result.ClearedCount);
            int comboBonus = _scoreComboSystem.FigurePlaced(result);

            boosterView.SetBoosterEnabled(_scoreBoostSystem.IsBoosted);
            ScoreSystem.SetBoosterEnabled(_scoreBoostSystem.IsBoosted);
            ScoreSystem.SetCombo(_scoreComboSystem.Shape, _scoreComboSystem.Counter);

            ScoreSystem.AddSetScore(result.ClearedCount);
            scoreView.UpdateScore();

            comboView.UpdateBonus(_scoreComboSystem.IsActive, _scoreComboSystem.Shape, _scoreComboSystem.Counter);

            if (comboBonus > 0)
            {
                ScoreSystem.AddScore(comboBonus);
                comboView.PlayBonusFly(comboBonus, () => scoreView.UpdateScore());
            }

            OnFigureScored?.Invoke(result, comboBonus);
        }

        private void UpdateView()
        {
            boosterView.SetBoosterEnabled(false);
            _scoreBoostSystem.IsBoosted = false;
            scoreView.UpdateScore(false);
            comboView.UpdateBonus(_scoreComboSystem.IsActive, _scoreComboSystem.Shape, _scoreComboSystem.Counter);
        }
    }
}