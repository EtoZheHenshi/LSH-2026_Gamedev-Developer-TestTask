using System;
using UnityEngine;

namespace Code.Gameplay.Items.Coin
{
    public sealed class CoinView : MonoBehaviour
    {
        public event Action OnCollect;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnCollect?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}