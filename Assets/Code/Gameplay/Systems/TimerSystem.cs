using System;
using Code.Core.Services;
using Code.Core.Update;

namespace Code.Gameplay.Systems
{
    public sealed class TimerSystem : IService, ITickable
    {
        public event Action OnTimeChange;
        
        private float _time = 300;
        private bool _active;
        
        public float TimeLeft => _time;

        public void Tick(float deltaTime)
        {
            if (!_active) return;
            
            _time -= deltaTime;
            OnTimeChange?.Invoke();
        }
        
        public void Remove(float time)
        {
            _time -= time;
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
    }
}