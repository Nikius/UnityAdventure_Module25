using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace Project.Scripts
{
    public class MoveAgent: IMover
    {
        private const float VelocityThreshold = 0.001f;
        
        private readonly NavMeshAgent _agent;
        private readonly IMoveEvents _listener;
        private readonly IMoveView _moveView;
        
        private bool _isMoving;
        
        public MoveAgent(NavMeshAgent agent, IMoveEvents listener, GameObject destinationPointPrefab)
        {
            _agent = agent;
            _listener = listener;
            _moveView = new MoveView(destinationPointPrefab);
        }
        
        private bool IsDestinationPointReached()
        {
            return _agent.pathPending == false
                   && _agent.remainingDistance <= _agent.stoppingDistance
                   && (_agent.hasPath == false || _agent.velocity.sqrMagnitude <= VelocityThreshold);
        }

        
        
        public void Update()
        {
            if (_isMoving && IsDestinationPointReached())
            {
                _isMoving = false;
                _moveView.OnMoveComplete();
                _listener.OnMoveComplete();
            }
        }
        
        public void MoveTo(Vector3 targetPosition)
        {
            _isMoving = true;
            _agent.SetDestination(targetPosition);
            _moveView.OnMoveStarted(targetPosition);
            _listener.OnMoveStarted();

        }

        public void SetLowSpeed()
        {
            _agent.speed *= 0.5f;
        }

        public void Stop()
        {
            _agent.ResetPath();
        }
    }
}