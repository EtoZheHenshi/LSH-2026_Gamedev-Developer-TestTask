using System;
using UnityEngine;

namespace Code.Gameplay.Enemies.Behaviours
{
    public sealed class StompableView : MonoBehaviour
    {
        public event Action OnStomp;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("CharacterStompArea"))
            {
                OnStomp?.Invoke();
            }
        }
    }
}