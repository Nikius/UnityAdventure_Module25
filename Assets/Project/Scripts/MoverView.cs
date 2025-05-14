using UnityEngine;

namespace Project.Scripts
{
    public class MoverView: IMoveView
    {
        private readonly GameObject _destinationPointPrefab;
        
        private GameObject _destinationPoint;

        public MoverView(GameObject destinationPointPrefab)
        {
            _destinationPointPrefab = destinationPointPrefab;
        }
        
        private void DestroyDestinationPoint()
        {
            Object.Destroy(_destinationPoint);
            _destinationPoint = null;
        }

        private void SetDestinationPoint(Vector3 targetPosition)
        {
            if (_destinationPoint is not null)
                DestroyDestinationPoint();
            
            _destinationPoint = Object.Instantiate(_destinationPointPrefab, targetPosition, Quaternion.identity);
        }

        public void OnMoveStarted(Vector3 targetPosition)
        {
            SetDestinationPoint(targetPosition);
        }

        public void OnMoveComplete()
        {
            DestroyDestinationPoint();
        }
    }
}