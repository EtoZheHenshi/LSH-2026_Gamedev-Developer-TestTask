using Code.Core.Services;
using Code.Gameplay.Systems;
using UnityEngine;

namespace Code.UI
{
    public sealed class UIController
    {
        private HUDModel _hud;
        public UIController(HUDView hudView)
        {
            _hud = new HUDModel(ServiceLocator.Get<ScoreSystem>(), 
                ServiceLocator.Get<CoinSystem>(), 
                ServiceLocator.Get<TimerSystem>(),
                hudView.ScoreCountText, 
                hudView.CoinCountText,
                hudView.TimeText
                );
        }
        public void Activate()
        {
            Time.timeScale = 0;
        }
    }
}