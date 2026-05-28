using Code.Gameplay.Systems;
using TMPro;

namespace Code.UI
{
    public sealed class HUDModel
    {
        private readonly ScoreSystem _scoreSystem;
        private readonly CoinSystem _coinSystem;
        private readonly TimerSystem _timerSystem;
        
        private readonly TMP_Text _scoreCountText;
        private readonly TMP_Text _coinCountText;
        private readonly TMP_Text _timeText;

        public HUDModel(ScoreSystem scoreSystem, CoinSystem coinSystem, TimerSystem timerSystem,  
            TMP_Text scoreCountText, TMP_Text coinCountText, TMP_Text timeText)
        {
            _scoreSystem = scoreSystem;
            _coinSystem = coinSystem;
            _timerSystem = timerSystem;
            _scoreCountText = scoreCountText;
            _coinCountText = coinCountText;
            _timeText = timeText;
            
            _scoreSystem.OnScoreUpdate += ScoreUpdate;
            _coinSystem.OnCoinCountUpdate += CoinUpdate;
            _timerSystem.OnTimeChange += TimeUpdate;
            
            ScoreUpdate();
            CoinUpdate();
            TimeUpdate();
        }

        private void ScoreUpdate()
        {
            _scoreCountText.text = _scoreSystem.Score.ToString();
        }

        private void CoinUpdate()
        {
            _coinCountText.text = _coinSystem.CoinCount.ToString();
        }

        private void TimeUpdate()
        {
            _timeText.text = ((int)_timerSystem.TimeLeft).ToString();
        }
        
        ~HUDModel()
        {
            _scoreSystem.OnScoreUpdate -= ScoreUpdate;
            _coinSystem.OnCoinCountUpdate -= CoinUpdate;
            _timerSystem.OnTimeChange -= TimeUpdate;
        }
    }
}