using Code.Gameplay.General;
using UnityEngine;

namespace Code.Gameplay.Enemies.Behaviours
{
    public sealed class PatrolMovementModel
    {
        private readonly MovementModel _movementModel;
        
        private float _direction = -1f;
        private readonly int _wallLayerID = LayerMask.NameToLayer("Wall");

        public PatrolMovementModel(Rigidbody2D rigidbody, float speed, float gravityModifier, 
            BoxLayerCheckModel groundedCheckModel)
        {
            _movementModel = new MovementModel(rigidbody, speed, gravityModifier, groundedCheckModel);
        }

        public void Tick(float deltaTime)
        {
            _movementModel.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            Patrol(deltaTime);
        }

        public void SwitchDirection(Collision2D collision)
        {
            if (collision.collider.gameObject.layer == _wallLayerID)
            {
                _direction = -_direction;
            }
        }

        private void Patrol(float deltaTime)
        {
            _movementModel.MoveHorizontal(deltaTime, _direction);
        }
    }
}