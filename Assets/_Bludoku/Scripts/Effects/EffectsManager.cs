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
        [SerializeField] private ParticleSystem comboAmbientParticles;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private SpriteRenderer boardGlow;

        private ParticleEffect _particleEffect;
        private ComboParticleEffect _comboParticleEffect;
        private ScreenShakeEffect _screenShakeEffect;
        private VibrationEffect _vibrationEffect;
        private ComboGlowEffect _comboGlowEffect;
        private ComboAmbientEffect _comboAmbientEffect;

        private void Awake()
        {
            _particleEffect = new ParticleEffect(particles);
            _comboParticleEffect = new ComboParticleEffect(comboParticles);
            _screenShakeEffect = new ScreenShakeEffect(cameraTransform);
            _vibrationEffect = new VibrationEffect();
            _comboGlowEffect = new ComboGlowEffect(boardGlow);
            _comboAmbientEffect = new ComboAmbientEffect(comboAmbientParticles);

            scoreMediator.OnFigureScored += OnFigureScored;
            scoreMediator.OnComboCounterChanged += OnComboChanged;
            scoreMediator.OnScoreReset += _comboAmbientEffect.Clear;
        }

        private void OnFigureScored(ClearResult result, int comboBonus)
        {
            _vibrationEffect.Play(result);

            if (comboBonus > 0)
            {
                _comboParticleEffect.Play(result, comboBonus);
                _screenShakeEffect.Play(comboBonus);
                _comboAmbientEffect.PlayBurst();
            }
            else
            {
                _particleEffect.Play(result);
            }
        }

        private void OnComboChanged(int comboCounter)
        {
            _comboGlowEffect.SetIntensity(comboCounter);
            _comboAmbientEffect.SetIntensity(comboCounter);
        }

        private void OnDisable()
        {
            _screenShakeEffect.Stop();
            _comboGlowEffect.Stop();
        }
    }
}
