using UnityEngine;

namespace Project.Scripts
{
    public class CombinedCharacterController: Controller
    {
        private readonly Character _character;
        private readonly float _idleMaxTime;
        
        private readonly PlayerCharacterController _playerCharacterController;
        private readonly RandomCharacterController _randomCharacterController;
        
        private float _idleTime;

        public CombinedCharacterController(Character character, float maxRandomRange, float idleMaxTime)
        {
            _character = character;
            _idleMaxTime = idleMaxTime;
            
            _playerCharacterController = new PlayerCharacterController(_character);
            _playerCharacterController.Enable();
            
            _randomCharacterController = new RandomCharacterController(_character, maxRandomRange);
        }

        protected override void UpdateLogic()
        {
            ManageControllers();

            _playerCharacterController.Update();
            _randomCharacterController.Update();
            
            _character.Update();
        }

        private void ManageControllers()
        {
            if (_idleTime >= _idleMaxTime)
            {
                _idleTime = 0;
                _randomCharacterController.Enable();
                _randomCharacterController.UpdateZeroPosition();
                return;
            }
            
            if (_character.IsMoving())
            {
                _idleTime = 0;
                _randomCharacterController.Disable();
                return;
            }
            
            _idleTime += Time.deltaTime;
        }
    }
}