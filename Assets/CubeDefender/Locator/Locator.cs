using System;
using System.Collections.Generic;

namespace CubeDefender.Locator
{
    public sealed class Locator<T>
        where T : class
    {
        private IDictionary<Type, T> _bank;

        public Locator()
        {
            _bank = new Dictionary<Type, T>();
        }

        public void Add<K>(K objectImplementation)
           where K : class, T
        {
            if (_bank.ContainsKey(typeof(K))) return;

            _bank.Add(typeof(K), objectImplementation);
        }

        public K Get<K>()
            where K : class, T
        {
            if (!_bank.TryGetValue(typeof(K), out var objectImplemetation))
                throw new NotImplementedException($"[Locator] Not contain object - {typeof(K).Name}");

            return objectImplemetation as K;
        }
    }
}
