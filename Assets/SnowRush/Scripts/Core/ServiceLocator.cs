using System;
using System.Collections.Generic;

namespace SnowRush.Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new();

        public static void Register<T>(T service) where T : class => Services[typeof(T)] = service;
        public static T Get<T>() where T : class => Services.TryGetValue(typeof(T), out var service) ? (T)service : null;
        public static void Clear() => Services.Clear();
    }
}
