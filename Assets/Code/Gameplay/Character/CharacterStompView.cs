using System;
using UnityEngine;

namespace Code.Gameplay.Character
{
    public sealed class CharacterStompView : MonoBehaviour
    {
        public event Action OnStomp;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Stompable"))
            {
                OnStomp?.Invoke();
            }
        }
    }
}