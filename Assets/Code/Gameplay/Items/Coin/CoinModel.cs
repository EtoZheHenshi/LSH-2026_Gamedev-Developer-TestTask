using Code.Core.Events;
using UnityEngine;

namespace Code.Gameplay.Items.Coin
{
    public sealed class CoinModel
    {
        private readonly EventBus _events;
        
        public CoinModel(EventBus eventBus)
        {
            _events = eventBus;
        }

        public void CoinCollect()
        {
            _events.Publish(new CoinCollectedEvent());
        }
    }
}