using UnityEngine;

namespace Code.Gameplay.Character
{
    public sealed class CharacterView : MonoBehaviour
    {
        [SerializeField] private CharacterConfig _config;
        [SerializeField] private Rigidbody2D _rb;
        
        public Rigidbody2D Rigidbody => _rb;
        public CharacterConfig Config => _config;
    }
}