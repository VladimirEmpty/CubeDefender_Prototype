using System;

using CubeDefender.StateMachine.State;

namespace CubeDefender.StateMachine
{
    public static class StateMachineExtention
    {
        public static void ChangeState<O, T>(this O owner, Action<T> onSetupStateCallback = null)
            where O : class, IStateMachineOwner
            where T : BaseState<O>, new()
        {
            owner.ChangeState<T>(state =>
            {
                state.StateOwner = owner;
                onSetupStateCallback?.Invoke(state);
            });
        }
    }
}
