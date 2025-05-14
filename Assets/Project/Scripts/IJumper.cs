using UnityEngine.AI;

namespace Project.Scripts
{
    public interface IJumper
    {
        public void Jump(OffMeshLinkData offMeshLinkData);
        
        public bool IsInProcess();
    }
}