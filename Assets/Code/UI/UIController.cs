using System;
using Code.Core.Events;
using Code.Core.Services;
using Code.Gameplay.Systems;
using UnityEngine;

namespace Code.UI
{
    public sealed class UIController : IDisposable
    {
        private HUDModel _hud;
        private readonly WinWindowView _winWindowView;
        
        public UIController(HUDView hudView, WinWindowView winView)
        {
            _hud = new HUDModel(ServiceLocator.Get<ScoreSystem>(), 
                ServiceLocator.Get<CoinSystem>(), 
                ServiceLocator.Get<TimerSystem>(),
                hudView.ScoreCountText, 
                hudView.CoinCountText,
                hudView.TimeText
                );
            
            _winWindowView = winView;
            
            ServiceLocator.Get<EventBus>().Subscribe<LevelFinishedEvent>(ShowWinWindow);
        }
        
        public void Activate()
        {
            Time.timeScale = 0;
        }

        private void ShowWinWindow(LevelFinishedEvent e)
        {
            _winWindowView.gameObject.SetActive(true);
        }

        public void Dispose()
        {
            ServiceLocator.Get<EventBus>().Unsubscribe<LevelFinishedEvent>(ShowWinWindow);
        }
    }
}