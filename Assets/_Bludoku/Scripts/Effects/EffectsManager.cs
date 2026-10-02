using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Score;
using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private ScoreMediator scoreMediator;
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private ParticleSystem comboParticles;

        private ParticleEffect _particleEffect;
        private ParticleEffect _comboParticleEffect;
        private VibrationEffect _vibrationEffect;
        
        private void Awake()
        {
            _particleEffect = new ParticleEffect(particles);
            _comboParticleEffect = new ParticleEffect(comboParticles);
            _vibrationEffect = new VibrationEffect();

            scoreMediator.OnFigureScored += OnFigureScored;
        }

        private void OnFigureScored(ClearResult result, int comboBonus)
        {
            _vibrationEffect.Play(result);

            if (comboBonus > 0)
            {
                _comboParticleEffect.Play(result);
            }
            else
            {
                _particleEffect.Play(result);
            }
        }
    }
}
