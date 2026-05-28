using Code.Core.Services;
using Code.Gameplay.Systems;
using UnityEngine.SceneManagement;

namespace Code.Gameplay.General
{
    public static class RestartLevel
    {
        private static TimerSystem _timerSystem;
        private static ScoreSystem _scoreSystem;
        private static CoinSystem _coinSystem;

        static RestartLevel()
        {
            _timerSystem = ServiceLocator.Get<TimerSystem>();
            _scoreSystem = ServiceLocator.Get<ScoreSystem>();
            _coinSystem = ServiceLocator.Get<CoinSystem>();
        }
        
        public static void Restart(bool refreshCoin)
        {
            _timerSystem.RefreshTimer();
            _scoreSystem.RefreshScore();
            if (refreshCoin)
            {
                _coinSystem.RefreshCoins();
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}