using DG.Tweening;
using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class ComboGlowEffect
    {
        private readonly SpriteRenderer _glow;
        private Tween _tween;

        private const float MinAlpha = 0.05f;
        private const float MaxAlpha = 0.3f;
        private const float FadeDuration = 0.3f;
        private const float PulseDuration = 0.6f;
        private const float PulseDepth = 0.5f;
        private const int FullIntensityCounter = 10;

        public ComboGlowEffect(SpriteRenderer glow)
        {
            _glow = glow;
            SetAlpha(0f);
        }

        public void SetIntensity(int comboCounter)
        {
            float intensity = Mathf.Clamp01((float)comboCounter / FullIntensityCounter);

            _tween?.Kill();

            if (intensity <= 0f)
            {
                _tween = _glow.DOFade(0f, FadeDuration);
                return;
            }

            float alpha = Mathf.Lerp(MinAlpha, MaxAlpha, intensity);
            _tween = _glow.DOFade(alpha, FadeDuration)
                .OnComplete(() =>
                {
                    _tween = _glow.DOFade(alpha * PulseDepth, PulseDuration)
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);
                });

        }

        public void Stop()
        {
            _tween?.Kill();
            _tween = null;
        }

        private void SetAlpha(float alpha)
        {
            Color color = _glow.color;
            color.a = alpha;
            _glow.color = color;
        }
    }
}