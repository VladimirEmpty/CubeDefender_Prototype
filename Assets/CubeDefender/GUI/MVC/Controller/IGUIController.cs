using CubeDefender.GUI.MVC.View;
using CubeDefender.GUI.MVC.Model;

namespace CubeDefender.GUI.MVC.Controller
{
    /// <summary>
    /// Основной интерфейс по работе с коммуникаций объектов View и Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="M"></typeparam>
    public interface IGUIController<T, M> : IController
        where T : class, IView
        where M : class, IModel, new()
    {
        T LinkedView { get; }
        M Model { get; }
        void UpdateView();
    }
}
