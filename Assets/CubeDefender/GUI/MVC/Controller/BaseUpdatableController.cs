using CubeDefender.GUI.MVC.View;
using CubeDefender.GUI.MVC.Model;

namespace CubeDefender.GUI.MVC.Controller
{
    public abstract class BaseUpdatableController<T, M> : BaseController<T, M>, IUpdatableController
        where T : class, IView
        where M : class, IModel, new()
    {
        public abstract string Tag { get; }

        public void UpdateController(string updateTag)
        {
            if (!string.IsNullOrEmpty(updateTag) & !Tag.Equals(updateTag))
                return;

            UpdateView();
        }
    }
}
