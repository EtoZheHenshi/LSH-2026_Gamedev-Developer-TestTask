using System;
using Code.Core.Events;
using Code.Core.Services;
using Code.Gameplay.Items.Coin;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class CoinSystem : IService
    {
        public event Action OnCoinCountUpdate;
        
        private int _coinCount;
        private readonly EventBus _eventBus;
        
        public int CoinCount => _coinCount;

        public CoinSystem(EventBus eventBus)
        {
            _eventBus = eventBus;
            eventBus.Subscribe<CoinCollectedEvent>(e => AddCoin());
        }

        private void AddCoin()
        {
            _coinCount++;
            OnCoinCountUpdate?.Invoke();
        }

        public void InitializeCoins(CoinView[] coins)
        {
            for (int i = 0; i < coins.Length; i++)
            {
                CoinController.Initialize(coins[i], _eventBus);
            }
        }
    }
}