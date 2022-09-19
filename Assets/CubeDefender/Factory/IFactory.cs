using System;

namespace CubeDefender.Factory
{
    /// <summary>
    /// Интерфейс по реализации одноразовой фабрики по создания объекта 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFactory<T> : IDisposable
        where T : class
    {
        T Prototype { get; }
        T Create();
    }
}
