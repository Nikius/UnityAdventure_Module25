using UnityEngine;

namespace Project.Scripts
{
    public class CharacterAnimator: IMoveEvents, IDamageEvents
    {
        private const float MinWeight = 0f;
        private const float MaxWeight = 1f;
        
        private const int BaseLayerKey = 0;
        private const int InjuredLayerKey = 1;

        private readonly int _isMovingKey = Animator.StringToHash("IsMoving");
        private readonly int _isDeadKey = Animator.StringToHash("IsDead");
        private readonly int _hitKey = Animator.StringToHash("Hit");

        private readonly Animator _animator;

        public CharacterAnimator(Animator animator)
        {
            _animator = animator;
            
            _animator.SetLayerWeight(BaseLayerKey, MaxWeight);
            _animator.SetLayerWeight(InjuredLayerKey, MinWeight);
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
            _animator.SetLayerWeight(BaseLayerKey, MinWeight);
            _animator.SetLayerWeight(InjuredLayerKey, MaxWeight);
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