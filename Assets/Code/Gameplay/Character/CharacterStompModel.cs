using UnityEngine;

namespace Code.Gameplay.Character
{
    public sealed class CharacterStompModel
    {
        private readonly Rigidbody2D _rb;
        private readonly float _stompJumpForce;

        public CharacterStompModel(Rigidbody2D rigidbody, float stompJumpForce)
        {
            _rb = rigidbody;
            _stompJumpForce = stompJumpForce;
        }

        public void Stomp()
        {
            _rb.linearVelocityY = 0f;
            _rb.AddForce(Vector2.up * _stompJumpForce, ForceMode2D.Impulse);
        }
    }
}