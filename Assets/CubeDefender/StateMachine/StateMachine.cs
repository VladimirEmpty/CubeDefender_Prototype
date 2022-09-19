using System;
using System.Collections.Generic;

using CubeDefender.Factory;
using CubeDefender.StateMachine.State;

namespace CubeDefender.StateMachine
{
    /// <summary>
    /// Данная сущность обеспечивает статичный доступ к работе StateMachine
    /// Основным пользователем является любой объект, реализующий интерфейс <see cref="IStateMachineOwner"/>
    /// </summary>
    public static class StateMachine
    {
        private static IDictionary<int, IMachine> _machineBank;
        private static GameUpdater _updater;

        static StateMachine()
        {
            _machineBank = new Dictionary<int, IMachine>();

            using(var factory = new GameObjectFactory<GameUpdater>())
            {
                _updater = factory.Create();
            }
        }

        public static void ChangeState<T>(this IStateMachineOwner owner, Action<T> onSetupStateCallback = null)
            where T : class, IState, new()
        {
            if(!_machineBank.TryGetValue(owner.Hash, out var machine))
            {
                using(var factory = new NativeObjectFactory<Machine>())
                {
                    machine = factory.Create();
                    _machineBank.Add(owner.Hash, machine);
                    _updater.OnUpdate += machine.Update;
                }
            }

            using(var factory = new NativeObjectFactory<T>())
            {
                var state = factory.Create();
                onSetupStateCallback?.Invoke(state);
                machine.ChangeState(state);
            }
        }

        public static bool IsContainState<T>(this IStateMachineOwner owner)
            where T : class, IState, new()
        {
            var result = false;

            if(_machineBank.TryGetValue(owner.Hash, out var machine) 
                    && machine.CurrentState != null)
            {
                result = machine.CurrentState.GetType().Equals(typeof(T));
            }

            return result;
        }

        public static void Reset(this IStateMachineOwner owner)
        {
            if(_machineBank.TryGetValue(owner.Hash, out var machine))
            {
                using(var factory = new NativeObjectFactory<NullState>())
                {
                    machine.ChangeState(factory.Create());
                }
            }
        }
    }
}
