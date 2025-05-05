using UnityEngine;

namespace Project.Scripts
{
    public class InputController
    {
        private const int LeftMouseButtonKey = 0;

        private readonly Camera _camera;
        private readonly IMover _mover;
        
        public InputController(IMover mover)
        {
            _camera = Camera.main;
            _mover = mover;
        }

        public void Update()
        {
            ProcessMouseInput();
            
            _mover.Update();
        }

        private void ProcessMouseInput()
        {
            if (!Input.GetMouseButtonDown(LeftMouseButtonKey))
                return;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;
                
            _mover.MoveTo(hit.point);
        }
    }
}
