using System;
using System.Collections;
using UnityEngine;

namespace Project.Scripts
{
    public class CharacterView: MonoBehaviour, IDamageEvents
    {
        private readonly int _isFlashingKey = Shader.PropertyToID("_IsFlashing");
        private readonly int _dissolveProgressKey = Shader.PropertyToID("_Progress");
        
        [SerializeField] private Material _dissolveMaterial;
        [SerializeField] private float _flashingDuration;
        [SerializeField] private float _dissolveDuration;
        [SerializeField] private float _secondsBeforeDissolve;

        private SkinnedMeshRenderer[] _objectRenderers;

        private void Awake()
        {
            _objectRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        }

        public void OnInjured()
        {
            
        }

        public void OnHit()
        {
            StopAllCoroutines();
            
            foreach (SkinnedMeshRenderer objectRenderer in _objectRenderers)
                StartCoroutine(Flash(objectRenderer.material, _flashingDuration));
        }

        public void OnDead()
        {
            StopAllCoroutines();
            
            foreach (SkinnedMeshRenderer objectRenderer in _objectRenderers)
            {
                objectRenderer.material = _dissolveMaterial;
                StartCoroutine(Dissolve(objectRenderer.material));
            }
                
        }
        
        private IEnumerator Flash(Material material, float waitTime)
        {
            material.SetInt(_isFlashingKey, 1);
            yield return new WaitForSeconds(waitTime);
            material.SetInt(_isFlashingKey, 0);
        }

        private IEnumerator Dissolve(Material material)
        {
            yield return new WaitForSeconds(_secondsBeforeDissolve);
            
            float progress = 0;
            
            while (progress <= _dissolveDuration)
            {
                material.SetFloat(_dissolveProgressKey, progress/_dissolveDuration);
                progress += Time.deltaTime;
                yield return null;
            }
        }
    }
}