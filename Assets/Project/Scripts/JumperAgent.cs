using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts
{
    public class JumperAgent: IJumper
    {
        private float _speed;
        private readonly NavMeshAgent _agent;
        private readonly AnimationCurve _yOffsetCurve;
        private readonly IJumpEvents _listener;
        private readonly MonoBehaviour _coroutineRunner;
        
        private Coroutine _jumpProcess;

        public JumperAgent(
            float speed,
            NavMeshAgent agent,
            MonoBehaviour coroutineRunner,
            AnimationCurve yOffsetCurve,
            IJumpEvents listener
        ) {
            _speed = speed;
            _agent = agent;
            _coroutineRunner = coroutineRunner;
            _yOffsetCurve = yOffsetCurve;
            _listener = listener;
        }
        
        public bool IsInProcess() => _jumpProcess != null;

        public void Jump(OffMeshLinkData offMeshLinkData)
        {
            if (IsInProcess())
                return;
            
            _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));
        }

        private IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
        {
            _listener.OnJumpStarted();
            
            Vector3 startPosition = offMeshLinkData.startPos;
            Vector3 endPosition = offMeshLinkData.endPos;
            
            float duration = Vector3.Distance(startPosition, endPosition) / _speed;
            float progress = 0;
            
            while (progress <= duration)
            {
                float yOffset = _yOffsetCurve.Evaluate(progress / duration);
                _agent.transform.position = Vector3.Lerp(startPosition, endPosition, progress / duration) + Vector3.up * yOffset;
                progress += Time.deltaTime;
                
                yield return null;
            }
            
            _agent.CompleteOffMeshLink();
            _jumpProcess = null;
            
            _listener.OnJumpComplete();
        }
    }
}