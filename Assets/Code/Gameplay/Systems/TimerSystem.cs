using System;
using Code.Core.Services;
using Code.Core.Update;

namespace Code.Gameplay.Systems
{
    public sealed class TimerSystem : IService, ITickable
    {
        public event Action OnTimeChange;
        
        private readonly float _time = 300;
        private float _timeLeft;
        private bool _active;
        
        public float TimeLeft => _timeLeft;

        public TimerSystem()
        {
            RefreshTimer();
        }

        public void Tick(float deltaTime)
        {
            if (!_active) return;
            
            _timeLeft -= deltaTime;
            OnTimeChange?.Invoke();
        }
        
        public void Remove(float time)
        {
            _timeLeft -= time;
            OnTimeChange?.Invoke();
        }
        
        public void StopTimer()
        {
            _active = false;
        }

        public void StartTimer()
        {
            _active = true;
        }
        
        public void RefreshTimer()
        {
            _timeLeft = _time;
        }
    }
}