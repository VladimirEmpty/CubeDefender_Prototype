using System;
using System.Collections.Generic;
using UnityEngine;

using CubeDefender.Chip;
using CubeDefender.GameSettings;

using Object = UnityEngine.Object;

namespace CubeDefender.Pool
{
    /// <summary>
    /// Базовый класс по реализации комплекса объектов пулов игровых объектов, объединенных одной сигнатурой запрашиваемого объекта
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class BasePoolComplex<K, T> : IContainablePool
        where T : BaseChip
    {
        private IDictionary<K, Pool> _chipPoolBank;
        private bool _isDisposed;

        public BasePoolComplex(GameResourcesSetting gameResourcesSetting)
        {
            GameResourcesSetting = gameResourcesSetting;

            _chipPoolBank = new Dictionary<K, Pool>();
        }

        protected GameResourcesSetting GameResourcesSetting { get; }

        public T Spawn(K key, Vector3 position)
        {
            if (!_chipPoolBank.TryGetValue(key, out var pool))
            {
                pool = new Pool(GetPrefab(key));
                _chipPoolBank.Add(key, pool);
            }

            return pool.Spawn(position);
        }

        public void Despawn(K key, T despawnedObject)
        {
            if (_chipPoolBank.TryGetValue(key, out var pool))
                pool.Despawn(despawnedObject);
            else
                Object.Destroy(despawnedObject.gameObject);
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            foreach (var el in _chipPoolBank)
            {
                el.Value.Dispose();
            }

            _chipPoolBank.Clear();
            _chipPoolBank = null;
            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        protected abstract T GetPrefab(K key);

        private sealed class Pool : BaseChipPool<T>
        {
            public Pool(T prototype) : base(prototype)
            {
            }
        }
    }
}
