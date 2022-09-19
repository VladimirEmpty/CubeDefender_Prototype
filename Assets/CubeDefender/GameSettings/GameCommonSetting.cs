using UnityEngine;

namespace CubeDefender.GameSettings
{
    [CreateAssetMenu(fileName = nameof(GameCommonSetting), menuName = "CubeDefender/Settings/GameCommon")]
    public sealed class GameCommonSetting : ScriptableObject
    {
        [Header("Player Settings")]
        [SerializeField] private int _enemyHealthModificator;
        [SerializeField] private int _enemyCountModificator;
        [SerializeField] private int _enemyStartCount;
        [SerializeField] private int _playerDamage;

        [Space(10f)]
        [Header("Bonus Chance Setting")]
        [SerializeField, Range(0f, 100f)] private float _createKeyChance;
        [SerializeField, Range(0f, 100f)] private float _createBonusChance;

        public int EnemyHealthModifire => _enemyHealthModificator;
        public int EnemyCreateCountModifer => _enemyCountModificator;
        public int EnemyStartCount => _enemyStartCount;
        public int PlayerDamage => _playerDamage;

        public float CreateKeyChance => _createKeyChance;
        public float CreateBonusChance => _createBonusChance;
    }
}
