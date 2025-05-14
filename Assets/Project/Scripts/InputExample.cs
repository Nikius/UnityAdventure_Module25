using UnityEngine;

namespace Project.Scripts
{
    public class InputExample: MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private float _maxRandomRange;
        [SerializeField] private float _idleMaxTime;
        
        private CombinedCharacterController _combinedCharacterController;

        private void Awake()
        {
            _combinedCharacterController = new CombinedCharacterController(_character, _maxRandomRange, _idleMaxTime);
            _combinedCharacterController.Enable();
        }

        private void Update()
        {
            _combinedCharacterController.Update();
        }
    }
}