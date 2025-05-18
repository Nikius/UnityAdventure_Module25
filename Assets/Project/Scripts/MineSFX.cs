using UnityEngine;

namespace Project.Scripts
{
    public class MineSFX: MonoBehaviour, IBlowProgress
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnActivated()
        {
            //
        }

        public void OnBlown()
        {
            PlayBoom();
        }
        
        private void PlayBoom()
        {
            AudioClip audioClip = _audioSource.clip;
            _audioSource.PlayOneShot(audioClip);
        }
    }
}