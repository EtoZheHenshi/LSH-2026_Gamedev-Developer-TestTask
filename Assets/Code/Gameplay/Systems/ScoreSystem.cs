using System;
using Code.Core.Events;
using Code.Core.Services;

namespace Code.Gameplay.Systems
{
    public sealed class ScoreSystem : IService
    {
        public event Action OnScoreUpdate;
        
        private int _score;
        
        public int Score => _score;

        public ScoreSystem(EventBus eventBus)
        {
            eventBus.Subscribe<CoinCollectedEvent>(OnCoinCollect);
        }

        public void Add(int score)
        {
            _score += score;
            OnScoreUpdate?.Invoke();
        }
        
        private void OnCoinCollect(CoinCollectedEvent e)
        {
            Add(100);
        }
    }
}