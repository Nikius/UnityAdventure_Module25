using System.Collections;
using UnityEngine;

namespace Project.Scripts
{
    public class MineView: MonoBehaviour, IBlowProgress
    {
        private const float MaxScale = 10f;
        
        private readonly int _colorKey = Shader.PropertyToID("_Color");
        private readonly int _visualScaleKey = Shader.PropertyToID("_VisualScale");
        
        [SerializeField] private ParticleSystem _particlesSystemPrefab;
        [SerializeField] private float _morphShapeSpeed = 5f;
        [SerializeField] private Color _morphColor = Color.red;
        [SerializeField] private float _morphColorSpeed = 2f;

        private MeshRenderer _meshRenderer;
        private Vector3 _visualScale;
        private Color _defaultColor;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _visualScale = _meshRenderer.material.GetVector(_visualScaleKey);
            _defaultColor = _meshRenderer.material.GetColor(_colorKey);
        }

        public void OnActivated()
        {
            ShowActivationVFX();
        }

        public void OnBlown()
        { 
            StopAllCoroutines();
            ShowBlowParticles();
            _meshRenderer.enabled = false;
        }
        
        private void ShowBlowParticles()
        {
            ParticleSystem blowVFX = Instantiate(_particlesSystemPrefab, transform.position, Quaternion.identity);
            blowVFX.Play();
            Destroy(blowVFX.gameObject, blowVFX.main.duration);
        }
        
        private void ShowActivationVFX()
        {
            StartCoroutine(MorphMineShape());
            StartCoroutine(MorphMineColor());
        }

        private IEnumerator MorphMineColor()
        {
            float time = 0f;
            
            while (time < _morphColorSpeed)
            {
                Color currentColor = Color.Lerp(_defaultColor, _morphColor, time / _morphColorSpeed);
                _meshRenderer.material.SetColor(_colorKey, currentColor);
                
                time += Time.deltaTime;
                yield return null;
            }
            
            _meshRenderer.material.SetColor(_colorKey, _morphColor);
        }

        private IEnumerator MorphMineShape()
        {
            while (true)
            {
                yield return StartCoroutine(AnimateComponent(value => _visualScale.x = value, _visualScale.x));
                yield return StartCoroutine(AnimateComponent(value => _visualScale.z = value, _visualScale.z));
            }
        }

        private IEnumerator AnimateComponent(System.Action<float> setComponent, float currentValue)
        {
            yield return StartCoroutine(LerpComponent(setComponent, currentValue, MaxScale));
            yield return StartCoroutine(LerpComponent(setComponent, MaxScale, currentValue));
        }

        private IEnumerator LerpComponent(System.Action<float> setComponent, float from, float to)
        {
            float duration = Mathf.Abs(to - from) / _morphShapeSpeed;
            float time = 0f;

            while (time < duration)
            {
                float value = Mathf.Lerp(from, to, time / duration);
                setComponent(value);
                time += Time.deltaTime;
                
                _meshRenderer.material.SetVector(_visualScaleKey, _visualScale);

                yield return null;
            }

            setComponent(to);
        }
    }
}