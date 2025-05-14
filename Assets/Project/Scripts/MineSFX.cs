using UnityEngine;

namespace Project.Scripts
{
    public class MineSFX
    {
        private readonly AudioSource _audioSource;

        public MineSFX(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void PlayBoom()
        {
            AudioClip audioClip = _audioSource.clip;
            _audioSource.PlayOneShot(audioClip);
        }
    }
}