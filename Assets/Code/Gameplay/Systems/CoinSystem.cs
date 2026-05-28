using Code.Core.Events;
using Code.Core.Services;
using Code.Gameplay.Items.Coin;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class CoinSystem : IService
    {
        private int _coinCount;
        private EventBus _eventBus;

        public CoinSystem(EventBus eventBus)
        {
            eventBus.Subscribe<CoinCollectedEvent>(e => AddCoin());
        }

        private void AddCoin()
        {
            _coinCount++;
            Debug.Log("Coin: " + _coinCount);
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