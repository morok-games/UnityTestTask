using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class ComboAmbientEffect
    {
        private readonly ParticleSystem _particles;
        private bool _isActive;

        private const float MinRate = 4f;
        private const float MaxRate = 20f;
        private const int FullIntensityCounter = 10;
        private const int StartBurstCount = 50;
        private const float BurstSpeedMultiplier = 4f;

        public ComboAmbientEffect(ParticleSystem particles)
        {
            _particles = particles;
            Clear();
        }

        public void SetIntensity(int comboCounter)
        {
            float intensity = Mathf.Clamp01((float)comboCounter / FullIntensityCounter);

            if (intensity <= 0f)
            {
                Clear();
                return;
            }

            var emission = _particles.emission;
            emission.rateOverTime = Mathf.Lerp(MinRate, MaxRate, intensity);

            if (!_isActive)
            {
                _isActive = true;
                _particles.Play();
                PlayBurst();
            }
        }

        public void PlayBurst()
        {
            var main = _particles.main;
            float speed = main.startSpeedMultiplier;

            main.startSpeedMultiplier = speed * BurstSpeedMultiplier;
            _particles.Emit(StartBurstCount);
            main.startSpeedMultiplier = speed;
        }

        private void Clear()
        {
            _isActive = false;
            _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}