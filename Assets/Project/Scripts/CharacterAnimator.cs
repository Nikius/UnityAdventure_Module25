using UnityEngine;

namespace Project.Scripts
{
    public class CharacterAnimator: IMoveEvents, IDamageEvents
    {
        private const float MinWeight = 0f;
        private const float MaxWeight = 1f;
        
        private readonly int _isMovingKey = Animator.StringToHash("IsMoving");
        private readonly int _isDeadKey = Animator.StringToHash("IsDead");
        private readonly int _hitKey = Animator.StringToHash("Hit");
        private readonly int _baseLayerKey = 0;
        private readonly int _injuredLayerKey = 1;
        
        private readonly Animator _animator;

        public CharacterAnimator(Animator animator)
        {
            _animator = animator;
            
            _animator.SetLayerWeight(_baseLayerKey, MaxWeight);
            _animator.SetLayerWeight(_injuredLayerKey, MinWeight);
        }

        public void OnMoveStarted()
        {
            _animator.SetBool(_isMovingKey, true);
        }

        public void OnMoveComplete()
        {
            _animator.SetBool(_isMovingKey, false);
        }
        
        public void OnInjured()
        {
            _animator.SetLayerWeight(_baseLayerKey, MinWeight);
            _animator.SetLayerWeight(_injuredLayerKey, MaxWeight);
        }

        public void OnHit()
        {
            _animator.SetTrigger(_hitKey);
        }

        public void OnDead()
        {
            _animator.SetBool(_isDeadKey, true);
        }
    }
}