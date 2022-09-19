using System;
using System.Collections.Generic;
using UnityEngine;

using CubeDefender.Factory;

using Object = UnityEngine.Object;

namespace CubeDefender.Pool
{
    /// <summary>
    /// Базовый класс пула игровых объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseGameObjectPool<T> : IPool<T>
        where T : Object
    {
        private Queue<T> _objectPool;
        private bool _isDisposed;

        public BaseGameObjectPool(T prototype)
        {
            _objectPool = new Queue<T>();

            Factory = new GameObjectFactory<T>(prototype);
            IsComponentDestroy = !typeof(T).IsSubclassOf(typeof(MonoBehaviour));
        }

        public IFactory<T> Factory { get; private set; }
        public virtual bool IsComponentDestroy { get; }

        public void Despawn(T despawnedObject)
        {
            OnDespawn(despawnedObject);
            _objectPool.Enqueue(despawnedObject);
        }

        public T Spawn()
        {
            var spawnedObject = _objectPool.Count > 0
                                            ? _objectPool.Dequeue()
                                            : Factory.Create();

            OnSpawn(spawnedObject);

            return spawnedObject;
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            Factory.Dispose();
            OnDispose();

            while (_objectPool.Count > 0)
            {
                Destroy(_objectPool.Dequeue());
            }

            Factory = null;
            _objectPool = null;
            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        protected abstract void OnSpawn(T spawnedObject);
        protected abstract void OnDespawn(T despawnedObject);
        protected virtual void OnDispose() { }

        protected void Destroy(T destractabelObject)
        {
            if (IsComponentDestroy)
            {
                Object.Destroy(destractabelObject);
            }
            else
            {
                var monoObject = destractabelObject as MonoBehaviour;
                Object.Destroy(monoObject.gameObject);
            }
        }
    }
}
