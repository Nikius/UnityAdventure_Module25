using UnityEngine;

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
            if (!Input.GetMouseButtonDown(LeftMouseButtonKey))
                return;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;
            
            _character.SetMoveTarget(hit.point);
        }
    }
}