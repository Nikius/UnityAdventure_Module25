using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Project.Scripts.Audio
{
    public class AudioMixerService
    {
        private const float MuteVolumeValue = -80f;
        private const float DefaultVolumeValue = 0f;
        
        private readonly AudioMixer _audioMixer;
        private readonly Dictionary<string, float> _startVolumeValues = new();
        private readonly string _volumeParameterName;

        private readonly Dictionary<string, bool> _isMutedByParameter = new();

        public AudioMixerService(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
        }
        
        public bool IsMuted(string key) => _isMutedByParameter.GetValueOrDefault(key, false);

        public void Mute(string groupName)
        {
            _audioMixer.GetFloat(groupName, out float startVolumeValue);
            _startVolumeValues[groupName] = startVolumeValue;
            
            _audioMixer.SetFloat(groupName, MuteVolumeValue);
            _isMutedByParameter[groupName] = true;
        }

        public void UnMute(string groupName)
        {
            float startVolumeValue = _startVolumeValues.GetValueOrDefault(groupName, DefaultVolumeValue);
            
            _audioMixer.SetFloat(groupName, startVolumeValue);
            _isMutedByParameter[groupName] = false;
        }
    }
}