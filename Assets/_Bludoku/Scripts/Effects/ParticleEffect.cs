using _Bludoku.Scripts.Boards;
using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class ParticleEffect
    {
        ParticleSystem _particle;
        
        public ParticleEffect(ParticleSystem particleSystem)
        {
            _particle = particleSystem;
        }
        
        public void Play(ClearResult result, float sizeMultiplier = 1f)
        {
            foreach (var pos in result.ClearedPositions)
            {
                var ps = Object.Instantiate(_particle, pos, Quaternion.identity);
                var main = ps.main;
                main.startSizeMultiplier *= sizeMultiplier;
                ps.Play();
                ps.gameObject.AddComponent<AutoDestroyParticle>();
            }
        }
    }
}
