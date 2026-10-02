using DG.Tweening;
using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class ScreenShakeEffect
    {
        private readonly Transform _cameraTransform;

        private const float Duration = 0.25f;
        private const int Vibrato = 20;
        private const float BaseStrength = 0.05f;
        private const float StrengthPerBonus = 0.015f;
        private const float MaxStrength = 0.2f;

        public ScreenShakeEffect(Transform cameraTransform)
        {
            _cameraTransform = cameraTransform;
        }

        public void Play(int bonus)
        {
            float strength = Mathf.Min(BaseStrength + bonus * StrengthPerBonus, MaxStrength);

            _cameraTransform.DOKill(true);
            _cameraTransform.DOShakePosition(Duration, new Vector3(strength, strength, 0f), Vibrato);
        }

        public void Stop()
        {
            _cameraTransform.DOKill(true);
        }
    }
}