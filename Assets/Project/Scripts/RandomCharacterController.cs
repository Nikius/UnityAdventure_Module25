using UnityEngine;

namespace Project.Scripts
{
    public class RandomCharacterController: Controller
    {
        private readonly Character _character;
        private readonly float _maxRange;
        
        private Vector3 _zeroPosition;

        public RandomCharacterController(Character character, float maxRange)
        {
            _character = character;
            _maxRange = maxRange;
        }

        protected override void UpdateLogic()
        {
            if (_character.IsMoving())
                return;
            
            Vector3 randomOffset = new Vector3(Random.Range(-_maxRange, _maxRange), 0, Random.Range(-_maxRange, _maxRange));
            _character.SetMoveTarget(_zeroPosition + randomOffset);
        }
        
        public void UpdateZeroPosition() => _zeroPosition = _character.transform.position;
    }
}