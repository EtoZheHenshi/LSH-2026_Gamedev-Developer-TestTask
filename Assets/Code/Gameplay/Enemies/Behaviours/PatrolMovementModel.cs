using Code.Gameplay.General;
using UnityEngine;

namespace Code.Gameplay.Enemies.Behaviours
{
    public sealed class PatrolMovementModel
    {
        private readonly MovementModel _movementModel;
        private readonly CircleLayerCheckModel _leftWallCheck;
        private readonly CircleLayerCheckModel _rightWallCheck;
        
        private float _direction = -1f;

        public PatrolMovementModel(Rigidbody2D rigidbody, float speed, float gravityModifier, 
            CircleLayerCheckModel groundedCheckModel, CircleLayerCheckModel leftWallCheck, 
            CircleLayerCheckModel rightWallCheck)
        {
            _movementModel = new MovementModel(rigidbody, speed, gravityModifier, groundedCheckModel);
            _leftWallCheck = leftWallCheck;
            _rightWallCheck = rightWallCheck;
        }

        public void Tick(float deltaTime)
        {
            _movementModel.Tick(deltaTime);
            if (_direction < 0)
            {
                SwitchDirection(_leftWallCheck);
            }
            else
            {
                SwitchDirection(_rightWallCheck);
            }
        }

        public void FixedTick(float deltaTime)
        {
            Patrol(deltaTime);
        }

        private void Patrol(float deltaTime)
        {
            _movementModel.MoveHorizontal(deltaTime, _direction);
        }
        
        private void SwitchDirection(CircleLayerCheckModel activeWallCheck)
        {
            activeWallCheck.Tick();
            if (activeWallCheck.IsCheckTrue)
            {
                _direction = -_direction;
            }
        }
    }
}