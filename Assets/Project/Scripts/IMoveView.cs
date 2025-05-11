using UnityEngine;

namespace Project.Scripts
{
    public interface IMoveView
    {
        void OnMoveStarted(Vector3 targetPosition);
        void OnMoveComplete();
    }
}