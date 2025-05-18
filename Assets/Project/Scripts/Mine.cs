using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class Mine: Enemy
    {
        private const float WaitUntilDestroy = 5f;
        
        [SerializeField] private float _blowStrength;
        [SerializeField] private float _timeToBlow;
        
        [SerializeField] private MineSFX _mineSFX;
        [SerializeField] private MineView _mineView;

        private bool _isActivated;
        
        private readonly List<IBlowProgress> _listeners = new();

        private void Awake()
        {
            _listeners.Add(_mineView);
            _listeners.Add(_mineSFX);
        }

        private void Update()
        {
            if (_isActivated)
                return;
            
            if (ShouldActivate())
            {
                StartCoroutine(BlowProcess());
                _isActivated = true;
            }
        }

        private IEnumerator BlowProcess()
        {
            foreach (IBlowProgress listener in _listeners)
                listener.OnActivated();
            
            yield return new WaitForSeconds(_timeToBlow);
            
            BlowNearObjects();
            BlowMine();

            yield return null;
        }

        private bool ShouldActivate()
        {
            Collider[] detectedObjects = DetectNearObjects();

            foreach (Collider detectedObject in detectedObjects)
            {
                if (detectedObject.GetComponent<IBlowable>() is not null)
                {
                    return true;
                }
            }
            
            return false;
        }

        private void BlowNearObjects()
        {
            Collider[] detectedObjects = DetectNearObjects();

            foreach (Collider detectedObject in detectedObjects)
            {
                IBlowable objectBlowable = detectedObject.GetComponent<IBlowable>();
                        
                if (objectBlowable is not null)
                    objectBlowable.OnBlow(transform.position, _blowStrength);
            }
        }
        
        private void BlowMine()
        {
            foreach (IBlowProgress listener in _listeners)
                listener.OnBlown();
            
            Destroy(gameObject, WaitUntilDestroy);
        }
    }
}