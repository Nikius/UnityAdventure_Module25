using UnityEngine;

namespace Project.Scripts
{
    public class MineView
    {
        private readonly ParticleSystem _blowVFXPrefab;

        public MineView(ParticleSystem blowVFXPrefab)
        {
            _blowVFXPrefab = blowVFXPrefab;
        }

        public void ShowBlowVFX(Vector3 position)
        {
            ParticleSystem blowVFX = Object.Instantiate(_blowVFXPrefab, position, Quaternion.identity);
            blowVFX.Play();
            Object.Destroy(blowVFX.gameObject, blowVFX.main.duration);
        }
    }
}