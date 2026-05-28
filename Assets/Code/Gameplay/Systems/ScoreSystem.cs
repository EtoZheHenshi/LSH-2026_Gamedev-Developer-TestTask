using Code.Core.Events;
using Code.Core.Services;

namespace Code.Gameplay.Systems
{
    public sealed class ScoreSystem : IService
    {
        private int _score;

        public ScoreSystem(EventBus eventBus)
        {
            eventBus.Subscribe<CoinCollectedEvent>(OnCoinCollect);
        }

        public void Add(int score)
        {
            _score += score;
        }
        
        private void OnCoinCollect(CoinCollectedEvent e)
        {
            _score += 100;
        }
    }
}