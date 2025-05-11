using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class InputExample: MonoBehaviour
    {
        private const int LeftMouseButtonKey = 0;
        
        [SerializeField] private Character _character;
        [SerializeField] private float _maxRandomRange;
        [SerializeField] private float _idleMaxTime;
        
        private PlayerCharacterController _playerCharacterController;
        private RandomCharacterController _randomCharacterController;
        private float _idleTime;

        private void Awake()
        {
            _playerCharacterController = new PlayerCharacterController(_character);
            _playerCharacterController.Enable();
            
            _randomCharacterController = new RandomCharacterController(_character, _maxRandomRange);
        }

        private void Update()
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