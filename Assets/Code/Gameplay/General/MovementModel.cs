using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class MovementModel
    {
        private Vector2 _velocity;
        private readonly float _speed;
        private readonly float _accel;
        private readonly float _fallSpeed;
        private readonly bool _isSpeedPermanent;
        private readonly GroundedCheckModel _groundedCheckModel;
        private readonly float _gravity = 10f;

        public MovementModel(Vector2 velocity, float speed, float fallSpeed, GroundedCheckModel groundedCheckModel, 
            bool isSpeedPermanent = true, float accel = 0f)
        {
            _velocity = velocity;
            _speed = speed;
            _fallSpeed = fallSpeed;
            _groundedCheckModel = groundedCheckModel;
            _isSpeedPermanent = isSpeedPermanent;
            _accel = accel;
        }

        public void Tick(float deltaTime)
        {
            Gravity(deltaTime);
        }

        public void MoveHorizontal(float deltaTime, float direction)
        {
            if (_isSpeedPermanent)
            {
                _velocity.x = direction * _speed * deltaTime;
            }
            else
            {
                _velocity.x = Mathf.MoveTowards(_velocity.x, direction * _speed, _accel * deltaTime);
            }
        }

        private void Gravity(float deltaTime)
        {
            if (!_groundedCheckModel.IsGrounded)
            {
                float fallSpeed = _velocity.y - _gravity * deltaTime;
                if (fallSpeed < _fallSpeed)
                {
                    _velocity.y = fallSpeed;
                }
                else
                {
                    _velocity.y = _fallSpeed;
                }
            }
        }
    }
}