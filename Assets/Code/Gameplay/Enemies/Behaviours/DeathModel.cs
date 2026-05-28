using System;

namespace Code.Gameplay.Enemies.Behaviours
{
    public sealed class DeathModel
    {
        public event Action OnDie;

        public void Die()
        {
            OnDie?.Invoke();
        }
    }
}