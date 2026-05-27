using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class MovementModel
    {
        private readonly Rigidbody2D _rb;
        private readonly float _speed;
        private readonly float _accel;
        private readonly float _gravityModifier;
        private readonly bool _isSpeedPermanent;
        private readonly BoxLayerCheckModel _groundedCheckModel;
        private readonly float _gravity = -5f;

        public MovementModel(Rigidbody2D rigidbody, float speed, float gravityModifier, BoxLayerCheckModel groundedCheckModel, 
            bool isSpeedPermanent = true, float accel = 0f)
        {
            _rb = rigidbody;
            _speed = speed;
            _gravityModifier = gravityModifier;
            _groundedCheckModel = groundedCheckModel;
            _isSpeedPermanent = isSpeedPermanent;
            _accel = accel;
        }

        public void Tick(float deltaTime)
        {
            _groundedCheckModel.Tick();
            Gravity(deltaTime);
        }

        public void MoveHorizontal(float deltaTime, float direction)
        {
            if (_isSpeedPermanent)
            {
                _rb.linearVelocityX = direction * _speed;
            }
            else
            {
                _rb.linearVelocityX = Mathf.MoveTowards(_rb.linearVelocityX, direction * _speed, 
                    _accel);
            }
        }

        private void Gravity(float deltaTime)
        {
            float gravity = _gravity * _gravityModifier;
            if (!_groundedCheckModel.IsCheckTrue && _rb.linearVelocityY > gravity)
            {
                _rb.linearVelocityY += gravity * deltaTime;
            }
        }
    }
}