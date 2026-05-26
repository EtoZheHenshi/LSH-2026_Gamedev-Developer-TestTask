using UnityEngine;

namespace Code.Gameplay.Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/Character/CharacterConfig")]
    public sealed class CharacterConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _gravityModifier = 1f;
        [SerializeField] private float _accel;
        [SerializeField] private float _jumpForce;
        [SerializeField] private int _maxJumps;
        [SerializeField] private int _stompJumpForce;
        
        public float Speed => _speed;
        public float GravityModifier => _gravityModifier;
        public float Accel => _accel;
        public float JumpForce => _jumpForce;
        public int MaxJumps => _maxJumps;
        public int StompJumpForce => _stompJumpForce;
    }
}