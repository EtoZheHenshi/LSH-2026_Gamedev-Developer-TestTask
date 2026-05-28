using System;
using UnityEngine;

namespace Code.Gameplay.General.LevelFinishLogic
{
    public sealed class FinishFlagView : MonoBehaviour
    {
        [SerializeField] private Transform _couchTransform;
        [SerializeField] private Transform _characterSprite;

        public event Action OnCharacterEntered;
        
        public Transform CouchTransform => _couchTransform;
        public Transform CharacterSprite => _characterSprite;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnCharacterEntered?.Invoke();
            }
        }
    }
}