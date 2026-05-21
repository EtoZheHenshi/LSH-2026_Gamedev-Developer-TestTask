using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class MovementModel
    {
        private Rigidbody2D _rb;
        private readonly float _speed;
        private readonly float _accel;
        private readonly float _fallSpeed;
        private readonly bool _isSpeedPermanent;
        private readonly GroundedCheckModel _groundedCheckModel;
        private readonly float _gravity = 10f;

        public MovementModel(Rigidbody2D rigidbody, float speed, float fallSpeed, GroundedCheckModel groundedCheckModel, 
            bool isSpeedPermanent = true, float accel = 0f)
        {
            _rb = rigidbody;
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
                _rb.MovePosition(new Vector2(_rb.position.x + direction * _speed * deltaTime, _rb.position.y));
            }
            else
            {
                _rb.MovePosition(new Vector2(
                    Mathf.MoveTowards(_rb.position.x, direction * _speed, _accel * deltaTime),
                    _rb.position.y
                    )
                );
            }
        }

        private void Gravity(float deltaTime)
        {
            if (!_groundedCheckModel.IsGrounded)
            {
                float fallSpeed = _rb.position.y - _gravity * deltaTime;
                if (fallSpeed < _fallSpeed)
                {
                    _rb.MovePosition(new Vector2(_rb.position.x, fallSpeed));
                }
                else
                {
                    _rb.MovePosition(new Vector2(_rb.position.x, _fallSpeed));
                }
            }
        }
    }
}