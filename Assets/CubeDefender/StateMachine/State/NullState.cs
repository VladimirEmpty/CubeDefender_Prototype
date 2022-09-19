using System;

namespace CubeDefender.StateMachine.State
{
    public sealed class NullState : IState
    {
        private bool _isDisposed;

        public bool IsUpdatable => false;

        public void Dispose()
        {
            if (_isDisposed) return;

            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        public void Enter()
        {            
        }

        public void Exit()
        {            
        }

        public void Update()
        {            
        }
    }
}
