using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts
{
    public class Character: MonoBehaviour, IBlowable, IDamageEvents
    {
        [SerializeField] private GameObject _destinationPointPrefab;
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private CharacterView _characterView;

        private Rigidbody _rigidbody;
        private CharacterAnimator _characterAnimator;
        private NavMeshAgent _agent;
        private IMover _mover;
        private IJumper _jumper;
        private Rotator _rotator;
        private Health _health;
        
        private bool _isDead;
        
        public bool InJumpProcess => _jumper.IsInProcess();

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _characterAnimator = new CharacterAnimator(_animator);
            _mover = new MoverAgent(_agent, _characterAnimator, _destinationPointPrefab);
            _jumper = new JumperAgent(_jumpSpeed, _agent, this, _jumpCurve, _characterAnimator);
            _rotator = new Rotator(_rotationSpeed, transform);

            IDamageEvents[] listeners = { _characterAnimator, this, _characterView }; 
            _health = new Health(listeners, _maxHealth);
        }

        public void Update()
        {
            _mover.Update();
            
            if (InJumpProcess)
                _rotator.Update();
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

        public void SetRotationDirection(Vector3 direction)
        {
            _rotator.SetDirection(direction);
        }

        public bool IsMoving()
        {
            return _mover.IsMoving();
        }

        public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
        {
            if (_agent.isOnOffMeshLink)
            {
                offMeshLinkData = _agent.currentOffMeshLinkData;
            
                return true;
            }
            
            offMeshLinkData = default;
            
            return false;
        }
        
        public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);
    }
}