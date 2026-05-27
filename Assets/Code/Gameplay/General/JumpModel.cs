using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class JumpModel
    {
        private readonly Rigidbody2D _rb;
        private readonly BoxLayerCheckModel _groundedCheckModel;
        private readonly float _jumpForce;
        private readonly int _maxJumps;
        
        private int _currentMaxJumps = 1;
        private int _jumpCount;
        private bool _isJumping;

        public JumpModel(Rigidbody2D rb, BoxLayerCheckModel groundedCheckModel, float jumpForce,  int maxJumps = 1)
        {
            _rb = rb;
            _groundedCheckModel = groundedCheckModel;
            _jumpForce = jumpForce;
            _maxJumps = maxJumps;
        }

        public void Tick()
        {
            if (!_groundedCheckModel.IsCheckTrue)
            {
                _isJumping = true;
                if (_jumpCount == 0)
                {
                    _jumpCount++;
                }
            }
            
            if (_isJumping && _groundedCheckModel.IsCheckTrue)
            {
                _isJumping = false;
                _jumpCount = 0;
            }
        }
        
        public void Jump()
        {
            if (_jumpCount < _currentMaxJumps)
            {
                _jumpCount++;
                _rb.linearVelocityY = 0f;
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        public void JumpUpgrade()
        {
            if (_currentMaxJumps < _maxJumps)
            {
                _currentMaxJumps++;
            }
        }
        
        public void JumpDowngrade()
        {
            if (_currentMaxJumps > 1)
            {
                _currentMaxJumps--;
            }
        }
        
    }
}