using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts
{
    public class Character: MonoBehaviour, IBlowable, IDamageEvents
    {
        [SerializeField] private GameObject _destinationPointPrefab;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _maxHealth;

        private Rigidbody _rigidbody;
        private CharacterAnimator _characterAnimator;
        private IMover _mover;
        private Health _health;
        
        private bool _isDead;

        private void Awake()
        {
            _characterAnimator = new CharacterAnimator(_animator);
            _mover = new MoveAgent(GetComponent<NavMeshAgent>(), _characterAnimator, _destinationPointPrefab);

            IDamageEvents[] listeners = { _characterAnimator, this }; 
            _health = new Health(listeners, _maxHealth);
        }

        public void Update()
        {
            _mover.Update();
        }

        public void OnBlow(Vector3 position, float power)
        {
            _health.TakeDamage(power);
        }
        
        public void OnInjured()
        {
            _mover.SetLowSpeed();
        }

        public void OnHit()
        {
            //
        }

        public void OnDead()
        {
            _mover.Stop();
            _isDead = true;
        }

        public void SetMoveTarget(Vector3 target)
        {
            if (_isDead == false)
                _mover.MoveTo(target);
        }

        public bool IsMoving()
        {
            return _mover.IsMoving();
        }
    }
}