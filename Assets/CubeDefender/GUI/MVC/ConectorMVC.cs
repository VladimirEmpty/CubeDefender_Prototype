using System;

using CubeDefender.GUI.MVC.Controller;
using CubeDefender.GUI.MVC.View;
using CubeDefender.Factory;

namespace CubeDefender.GUI.MVC
{
    /// <summary>
    /// Объект по статичному доступу к работе с UI элементами через использования упрощенной версии MVC. Основной пользователь, объект реализующий интерфейс <see cref="IView"/>
    /// </summary>
    /// <remarks>
    /// Примечание: Данная версия MVC является упрощенной, так как она не может создавать префабы View, 
    /// не имеет функционала взаимодействия с стандартными объектами Unity и TMPro, 
    /// а также работает только с одним объектом View
    /// </remarks>
    public static class ConectorMVC
    {
        private static event Action<string> OnUpdateControllers;
        private static event Action OnUpdateAllControllers;

        public static void AddController<T>(this IView view)
            where T : class, IController, new()
        {
            using(var factroy = new NativeObjectFactory<T>())
            {
                var controller = factroy.Create();
                controller.AddView(view);

                if(controller is IUpdatableController)
                {
                    var updatableController = controller as IUpdatableController;
                    OnUpdateControllers += updatableController.UpdateController;
                    OnUpdateAllControllers += () => { updatableController.UpdateController(updatableController.Tag); };
                }
            }
        }

        public static void UpdateAllController() => OnUpdateAllControllers?.Invoke();
        public static void UpdateController(string tag) => OnUpdateControllers?.Invoke(tag);
        public static void UpdateController<T>() where T : class, IController => OnUpdateControllers?.Invoke(typeof(T).Name);
    }
}
