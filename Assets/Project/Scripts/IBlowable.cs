using UnityEngine;

namespace Project.Scripts
{
    public interface IBlowable
    {
        public void OnBlow(Vector3 position, float power);
    }
}