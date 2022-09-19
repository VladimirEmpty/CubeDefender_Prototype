using System;
using System.Collections.Generic;

using CubeDefender.Pool;

namespace CubeDefender.Service
{
    /// <summary>
    /// Класс-сервис с логикой работы с пулами <see cref="IContainablePool"/>
    /// </summary>
    public sealed class PoolService : IService
    {
        private IDictionary<Type, IContainablePool> _poolBank;

        public PoolService()
        {
            _poolBank = new Dictionary<Type, IContainablePool>();
        }

        public void Add<T>(T pool)
            where T : class, IContainablePool
        {
            if(_poolBank.TryGetValue(typeof(T), out var containedPool))
                RemovePool(containedPool);

            _poolBank[typeof(T)] = pool;
        }

        public T Get<T>()
            where T : class, IContainablePool
        {
            if (_poolBank.TryGetValue(typeof(T), out var pool))
                return pool as T;
            else
                throw new NotImplementedException($"[PoolService]: Not contain pool with type - {typeof(T).Name}");
        }

        public void Remove<T>()
            where T : class, IContainablePool
        {
            if (_poolBank.TryGetValue(typeof(T), out var pool))
                RemovePool(pool);
        }

        public void RemoveAll()
        {
            foreach(var el in _poolBank)
            {
                el.Value.Dispose();
            }

            _poolBank.Clear();
        }

        private void RemovePool<T>(T pool)
            where T : class, IContainablePool
        {
            pool.Dispose();
            _poolBank.Remove(typeof(T));
        }
    }
}
