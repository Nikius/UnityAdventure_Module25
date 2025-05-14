using System;
using Project.Scripts.Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class VolumeToggler: MonoBehaviour
    {
        private const string MusicVolumeParamKey = "MusicVolume";
        private const string SFXVolumeParamKey = "SFXVolume";
        
        [SerializeField] private Button _button;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioMixerParamsEnum _audioMixerParameter;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;
        
        private Image _image;
        private AudioMixerService _audioMixerService;
        
        private string _volumeParameterName;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _audioMixerService = new AudioMixerService(_audioMixer);
            
            switch (_audioMixerParameter)
            {
                case AudioMixerParamsEnum.MusicVolume:
                    _volumeParameterName = MusicVolumeParamKey;
                    break;
                
                case AudioMixerParamsEnum.SFXVolume:
                    _volumeParameterName = SFXVolumeParamKey;
                    break;
                
                default:
                    Debug.LogError($"AudioMixerParameter {_audioMixerParameter} is not supported");
                    break;
            }
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (_audioMixerService.IsMuted(_volumeParameterName))
            {
                _image.sprite = _onSprite;
                _audioMixerService.UnMute(_volumeParameterName);
            }
            else
            {
                _image.sprite = _offSprite;
                _audioMixerService.Mute(_volumeParameterName);
            }
        }
    }
}