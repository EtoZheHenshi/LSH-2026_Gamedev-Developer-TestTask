using System.Collections.Generic;
using Code.Core.Services;

namespace Code.Core.Update
{
    public sealed class UpdateManager : IService
    {
        private readonly List<ITickable> _tickables = new();
        private readonly List<IFixedTickable> _fixedTickables = new();

        public void Register(ITickable tickable)
        {
            _tickables.Add(tickable);
        }

        public void Register(IFixedTickable fixedTickable)
        {
            _fixedTickables.Add(fixedTickable);
        }

        public void Tick(float deltaTime)
        {
            for (int i = 0; i < _tickables.Count; i++)
            {
                _tickables[i].Tick(deltaTime);
            }
        }

        public void FixedTick(float fixedDeltaTime)
        {
            for (int i = 0; i < _fixedTickables.Count; i++)
            {
                _fixedTickables[i].FixedTick(fixedDeltaTime);
            }
        }
    }
}