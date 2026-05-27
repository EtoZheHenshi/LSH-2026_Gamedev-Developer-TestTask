using System.Collections.Generic;
using Code.Core.Services;

namespace Code.Core.Update
{
    public sealed class UpdateManager : IService
    {
        private readonly List<ITickable> _tickables = new(8);
        private readonly List<IFixedTickable> _fixedTickables = new(8);
        private readonly List<ILateTickable> _lateTickables = new(8);

        public void Register(ITickable tickable)
        {
            _tickables.Add(tickable);
        }

        public void Register(IFixedTickable fixedTickable)
        {
            _fixedTickables.Add(fixedTickable);
        }
        
        public void Register(ILateTickable lateTickable)
        {
            _lateTickables.Add(lateTickable);
        }
        
        public void Remove(ITickable tickable)
        {
            _tickables.Remove(tickable);
        }

        public void Remove(IFixedTickable fixedTickable)
        {
            _fixedTickables.Remove(fixedTickable);
        }
        
        public void Remove(ILateTickable lateTickable)
        {
            _lateTickables.Remove(lateTickable);
        }

        public void Tick(float deltaTime)
        {
            for (int i = 0; i < _tickables.Count; i++)
            {
                _tickables[i]?.Tick(deltaTime);
            }
        }

        public void FixedTick(float fixedDeltaTime)
        {
            for (int i = 0; i < _fixedTickables.Count; i++)
            {
                _fixedTickables[i]?.FixedTick(fixedDeltaTime);
            }
        }
        
        public void LateTick(float deltaTime)
        {
            for (int i = 0; i < _lateTickables.Count; i++)
            {
                _lateTickables[i]?.LateTick(deltaTime);
            }
        }
    }
}