using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Project.Scripts
{
    public class PlayerCharacterController: Controller
    {
        private const int LeftMouseButtonKey = 0;

        private readonly Camera _camera;
        private readonly Character _character;

        public PlayerCharacterController(Character character)
        {
            _character = character;
            _camera = Camera.main;
        }

        protected override void UpdateLogic()
        {
            if (Input.GetMouseButtonDown(LeftMouseButtonKey) && EventSystem.current.IsPointerOverGameObject() == false)
                SetMoveTargetByMouse();

            if (_character.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData) && _character.InJumpProcess == false)
            {
                _character.SetRotationDirection(offMeshLinkData.endPos - offMeshLinkData.startPos);
                _character.Jump(offMeshLinkData);
            }
        }

        private void SetMoveTargetByMouse()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;
            
            _character.SetMoveTarget(hit.point);
        }
    }
}