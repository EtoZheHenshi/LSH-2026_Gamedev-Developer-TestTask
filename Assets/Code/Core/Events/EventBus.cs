using System;
using System.Collections.Generic;
using Code.Core.Services;

namespace Code.Core.Events
{
    public sealed class EventBus : IService
    {
        private readonly Dictionary<Type, Delegate> _events = new ();

        public void Subscribe<T>(Action<T> callback) where T : IEvent
        {
            Type type = typeof(T);

            if (_events.ContainsKey(type))
            {
                _events[type] = Delegate.Combine(_events[type], callback);
            }
            else
            {
                _events.Add(type, callback);
            }
        }

        public void Unsubscribe<T>(Action<T> callback) where T : IEvent
        {
            Type type = typeof(T);

            if (_events.TryGetValue(type, out Delegate action))
            {
                _events[type] = Delegate.Remove(action, callback);
            }
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            Type type = typeof(T);

            if (_events.TryGetValue(type, out Delegate action))
            {
                ((Action<T>)action)?.Invoke(@event);
            }
        }
    }
}