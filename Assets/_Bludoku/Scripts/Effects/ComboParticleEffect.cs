using _Bludoku.Scripts.Boards;
using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class ComboParticleEffect
    {
        private readonly ParticleEffect _particleEffect;

        private const float SizePerBonus = 0.1f;
        private const float MaxSizeMultiplier = 2f;

        public ComboParticleEffect(ParticleSystem particleSystem)
        {
            _particleEffect = new ParticleEffect(particleSystem);
        }

        public void Play(ClearResult result, int bonus)
        {
            float size = Mathf.Min(1f + bonus * SizePerBonus, MaxSizeMultiplier);
            _particleEffect.Play(result, size);
        }
    }
}