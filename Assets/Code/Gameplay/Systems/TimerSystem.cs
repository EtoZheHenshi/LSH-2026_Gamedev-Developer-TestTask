using Code.Core.Services;
using Code.Core.Update;

namespace Code.Gameplay.Systems
{
    public sealed class TimerSystem : IService, ITickable
    {
        private float _time = 100;
        
        public float TimeLeft => _time;

        public void Remove(float time)
        {
            _time -= time;
        }

        public void Tick(float deltaTime)
        {
            _time -= deltaTime;
        }
    }
}