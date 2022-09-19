using CubeDefender.GUI.MVC;
using CubeDefender.GUI.Screen.PlayerInfo;
using CubeDefender.GUI.Screen.PlayerMainButton;
using CubeDefender.GUI.Popup.RestartGame;
using CubeDefender.GameSettings;

namespace CubeDefender.Service
{
    /// <summary>
    /// Класс-сервис с логикой работы с игрока
    /// </summary>
    public sealed class PlayerService : IService
    {
        private readonly GameCommonSetting GameCommonSetting;

        private int _playerScore;
        private bool _isContainKey;

        public PlayerService(GameCommonSetting gameCommonSetting)
        {
            GameCommonSetting = gameCommonSetting;
            DamageValue = gameCommonSetting.PlayerDamage;
            EnemyStartCount = gameCommonSetting.EnemyStartCount;
            MaxPlayerDamage = DamageValue;

            IsContainKey = true;
        }

        public int DamageValue { get; }
        public int MaxPlayerDamage { get; private set; }
        public int EnemyWave { get; private set; }
        public int EnemyStartCount { get; }
        public int EnemyHealthModifire => EnemyWave / GameCommonSetting.EnemyHealthModifire;
        public int EnemyCountModifre => EnemyWave / GameCommonSetting.EnemyCreateCountModifer;
        public float CreateKeyChance => GameCommonSetting.CreateKeyChance;
        public float CreateBonusChance => GameCommonSetting.CreateBonusChance;
        public int PlayerScore 
        { 
            get => _playerScore;
            set
            {
                _playerScore = value;
                ConectorMVC.UpdateController<PlayerInfoController>();
            }
        }
        public bool IsContainKey 
        { 
            get => _isContainKey;
            set
            {
                _isContainKey = value;
                ConectorMVC.UpdateController<PlayerMainButtonController>();
            }
        }
        public bool IsGameOver { get; private set; }

        public void AddEnemyWave() => EnemyWave++;

        public void GameOver()
        {
            IsGameOver = true;
            ConectorMVC.UpdateController<RestartGameController>();
        }

        public void RefreshMaxPlayerDamage(int value)
        {
            if (MaxPlayerDamage < value)
                MaxPlayerDamage = value;
        }

        public void Clear()
        {
            EnemyWave = default;

            MaxPlayerDamage = DamageValue;
            PlayerScore = default;
            IsContainKey = true;
            IsGameOver = false;
        }
    }
}
