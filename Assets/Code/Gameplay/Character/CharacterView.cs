using System;
using UnityEngine;

namespace Code.Gameplay.Character
{
    public sealed class CharacterView : MonoBehaviour
    {
        [SerializeField] private CharacterConfig _config;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private CharacterStompView _stompView;
        
        public Rigidbody2D Rigidbody => _rb;
        public CharacterConfig Config => _config;
        public event Action OnStomp
        {
            add => _stompView.OnStomp += value;
            remove => _stompView.OnStomp -= value;
        }
    }
}