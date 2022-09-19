using System;

namespace CubeDefender.StateMachine.State
{
    /// <summary>
    /// Общий для всех State интерфейс, может быть обновляемая, так и нет
    /// </summary>
    public interface IState : IDisposable
    {
        bool IsUpdatable { get; }
        void Enter();
        void Update();
        void Exit();
    }
}
