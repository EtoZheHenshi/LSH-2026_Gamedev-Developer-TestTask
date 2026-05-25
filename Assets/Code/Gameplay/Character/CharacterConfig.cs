using UnityEngine;

namespace Code.Gameplay.Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/Character/CharacterConfig")]
    public sealed class CharacterConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _gravityModifier = 1f;
        [SerializeField] private float _accel;
        
        public float Speed => _speed;
        public float GravityModifier => _gravityModifier;
        public float Accel => _accel;
    }
}