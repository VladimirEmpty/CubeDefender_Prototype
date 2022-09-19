using CubeDefender.StateMachine;
using CubeDefender.StateMachine.State.GameProcessorState;

namespace CubeDefender
{
    /// <summary>
    /// Класс реализующий общей игровой процесс, через работы StateMachine
    /// </summary>
    public sealed class GameProcessor : IStateMachineOwner
    {
        private static GameProcessor _instance;

        private GameProcessor()
        {
        }

        public static GameProcessor Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameProcessor();

                return _instance;
            }
        }

        public int Hash => GetHashCode();

        public void Execute() => _instance.ChangeState<GameProcessor, PlayerShootState>();

        public void GameOver() => _instance.ChangeState<GameProcessor, GameOverState>();

        public void Restart() => _instance.ChangeState<GameProcessor, RestartState>();
    }
}
