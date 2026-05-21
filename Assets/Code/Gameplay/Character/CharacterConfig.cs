using UnityEngine;

namespace Code.Gameplay.Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/Character/CharacterConfig")]
    public sealed class CharacterConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private float _accel;
        
        public float Speed => _speed;
        public float FallSpeed => _fallSpeed;
        public float Accel => _accel;
    }
}