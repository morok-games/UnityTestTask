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
        [SerializeField] private Transform cameraTransform;

        private ParticleEffect _particleEffect;
        private ComboParticleEffect _comboParticleEffect;
        private ScreenShakeEffect _screenShakeEffect;
        private VibrationEffect _vibrationEffect;
        
        private void Awake()
        {
            _particleEffect = new ParticleEffect(particles);
            _comboParticleEffect = new ComboParticleEffect(comboParticles);
            _screenShakeEffect = new ScreenShakeEffect(cameraTransform);
            _vibrationEffect = new VibrationEffect();

            scoreMediator.OnFigureScored += OnFigureScored;
        }

        private void OnFigureScored(ClearResult result, int comboBonus)
        {
            _vibrationEffect.Play(result);

            if (comboBonus > 0)
            {
                _comboParticleEffect.Play(result, comboBonus);
                _screenShakeEffect.Play(comboBonus);
            }
            else
            {
                _particleEffect.Play(result);
            }
        }

        private void OnDisable()
        {
            _screenShakeEffect.Stop();
        }
    }
}
