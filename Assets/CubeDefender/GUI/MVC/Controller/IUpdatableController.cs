namespace CubeDefender.GUI.MVC.Controller
{
    /// <summary>
    /// Основной интерфейс обновляемого Controller
    /// </summary>
    public interface IUpdatableController
    {
        string Tag { get; }

        void UpdateController(string updateTag);
    }
}
