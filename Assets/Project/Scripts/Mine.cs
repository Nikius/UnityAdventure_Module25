using System.Collections;
using UnityEngine;

namespace Project.Scripts
{
    public class Mine: Enemy
    {
        [SerializeField] private float _blowStrength;
        [SerializeField] private float _timeToBlow;
        [SerializeField] private ParticleSystem _blowVFXPrefab;
        [SerializeField] private AudioSource _audioSource;

        private bool _isActivated;
        
        private MineView _mineView;
        private MineSFX _mineSFX;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _mineView = new MineView(_blowVFXPrefab);
            _mineSFX = new MineSFX(_audioSource);
            _meshRenderer = GetComponent<MeshRenderer>();
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
            _meshRenderer.enabled = false;
            _mineView.ShowBlowVFX(transform.position);
            _mineSFX.PlayBoom();
            Destroy(gameObject, _audioSource.clip.length);
        }
    }
}