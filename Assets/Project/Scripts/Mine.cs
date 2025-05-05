using UnityEngine;

namespace Project.Scripts
{
    public class Mine: Enemy
    {
        [SerializeField] private float _blowStrength;
        [SerializeField] private float _timeToBlow;
        [SerializeField] private ParticleSystem _blowVFXPrefab;

        private float _timer;
        private bool _isActivated;

        private void Awake()
        {
            _timer = _timeToBlow;
        }

        private void Update()
        {
            if (_isActivated == false)
            {
                _isActivated = ShouldActivate();
            }
            else
            {
                _timer -= Time.deltaTime;

                if (_timer <= 0)
                {
                    BlowNearObjects();
                    BlowMine();
                }
            }
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
            ShowBlowVFX(transform.position);
            Destroy(gameObject);
        }

        private void ShowBlowVFX(Vector3 position)
        {
            ParticleSystem blowVFX = Instantiate(_blowVFXPrefab, position, Quaternion.identity);
            blowVFX.Play();
            Destroy(blowVFX.gameObject, blowVFX.main.duration);
        }
    }
}