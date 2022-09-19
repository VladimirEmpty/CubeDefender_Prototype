using System;
using UnityEngine;

using CubeDefender.Service;
using CubeDefender.Factory;

namespace CubeDefender.Locator
{
    /// <summary>
    /// Данная сущность выполняет роль DI контейнера, для реализации зависимостей для основных участников прототип. 
    /// Данную сущность полностью замещает любая DLL с реализацией DI подхода.
    /// </summary>
    public static class ServiceLocator
    {
        private static Locator<IService> _locator;

        static ServiceLocator()
        {
            using(var factory = new NativeObjectFactory<Locator<IService>>())
            {
                _locator = factory.Create();
            }
        }

        public static void Add<T>(Action<T> onSetupCallback = null)
            where T : class ,IService
        {
            using (var factory = new NativeObjectFactory<T>())
            {
                var objectImplementation = factory.Create();
                onSetupCallback?.Invoke(objectImplementation);
                _locator.Add(objectImplementation);
            }
        }

        public static void Add<T>(T service)
            where T : class, IService
        {
            _locator.Add(service);
        }

        public static T Get<T>()
            where T : class, IService
        {
            return _locator.Get<T>();
        }
    }
}
