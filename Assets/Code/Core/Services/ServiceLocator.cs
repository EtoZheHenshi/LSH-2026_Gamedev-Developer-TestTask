using System;
using System.Collections.Generic;

namespace Code.Core.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, IService> Services = new();

        public static void Register<T>(T service) where T : IService
        {
            Type type = typeof(T);

            Services.TryAdd(type, service);
        }

        public static T Get<T>() where T : IService
        {
            return (T)Services.GetValueOrDefault(typeof(T));
        }
    }
}