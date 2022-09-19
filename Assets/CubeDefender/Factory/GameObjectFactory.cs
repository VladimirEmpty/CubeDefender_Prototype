using System;
using UnityEngine;

using Object = UnityEngine.Object;

namespace CubeDefender.Factory
{
    /// <summary>
    /// Простая фабрика по создания GameObject в игре. Если фабрика не содержит прототип/префаб создаваемого объекта, то будет создан новый GameObject с указанным типом компонента.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GameObjectFactory<T> : IFactory<T>
        where T : class
    {
        private bool _isDisposed;

        public GameObjectFactory(T prototype = null)
        {
            Prototype = prototype;

            if (!typeof(T).IsSubclassOf(typeof(Object)))
                throw new NotSupportedException($"[GameObjectFactory]: Factory can't work with not Unity Object, try add object typeof - {typeof(T).Name}");
        }

        public T Prototype { get; private set; }

        public T Create()
        {
            return Prototype == null
                            ? (T)CreateObjectWithComponent(typeof(T))
                            : (T)CreateNewObject();
        }
                
        private object CreateObjectWithComponent(Type type) => new GameObject(type.Name).AddComponent(type);
        private object CreateNewObject()
        {
            var convertedPrototype = Prototype as Object;
            var result = Object.Instantiate(convertedPrototype);
            result.name = convertedPrototype.name;

            return result;
        }
        public void Dispose()
        {
            if (_isDisposed) return;

            Prototype = null;

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
