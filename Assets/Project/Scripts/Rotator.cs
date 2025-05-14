using UnityEngine;

namespace Project.Scripts
{
    public class Rotator
    {
        private const float RotationThreshold = 0.0001f;
        
        private readonly float _rotationSpeed;
        private readonly Transform _transform;
        
        private Vector3 _direction;

        public Rotator(float rotationSpeed, Transform transform)
        {
            _rotationSpeed = rotationSpeed;
            _transform = transform;
        }
        
        private void RotateTo()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_direction);
            float step = _rotationSpeed * Time.deltaTime;
        
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
        }

        public void Update()
        {
            if (_direction.sqrMagnitude > RotationThreshold)
                RotateTo();
        }
        
        public void SetDirection(Vector3 direction) => _direction = direction;
    }
}