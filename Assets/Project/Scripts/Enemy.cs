using UnityEngine;

namespace Project.Scripts
{
    public class Enemy: MonoBehaviour
    {
        [SerializeField] private float _agroRadius;

        protected Collider[] DetectNearObjects()
        {
             return Physics.OverlapSphere(transform.position, _agroRadius);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _agroRadius);
        }
    }
}