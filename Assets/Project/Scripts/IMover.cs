using UnityEngine;

namespace Project.Scripts
{
    public interface IMover
    {
        void Update();
        void MoveTo(Vector3 targetPosition);
        bool IsMoving();
        void SetLowSpeed();
        void Stop();
    }
}