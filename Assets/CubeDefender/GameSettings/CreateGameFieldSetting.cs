using UnityEngine;

namespace CubeDefender.GameSettings
{
    [CreateAssetMenu(fileName = nameof(CreateGameFieldSetting), menuName = "CubeDefender/Settings/CreateGameField")]
    public sealed class CreateGameFieldSetting : ScriptableObject
    {
        [Header("Enemy")]
        [SerializeField] private Vector3 _enemyGameFieldPivotPosition;
        [SerializeField] private int _enemyGameFieldMaxSizeX;
        [SerializeField] private int _enemyGameFieldMaxSizeY;

        [Space(10f)]
        [Header("Player")]
        [SerializeField] private Vector3 _playerGameFieldPivotPosition;
        [SerializeField] private int _playerGameFieldMaxSizeX;
        [SerializeField] private int _playerGameFieldMaxSizeY;

        [SerializeField] private float _cellOffset;
        [SerializeField] private float _cellHight;
        

        public Vector3 EnemyGameFieldPivotPoint => _enemyGameFieldPivotPosition;
        public Vector3 PlayerGameFieldPivotPoint => _playerGameFieldPivotPosition;

        public int EnemyGameFieldMaxSizeX => _enemyGameFieldMaxSizeX;
        public int EnemyGameFieldMaxSizeY => _enemyGameFieldMaxSizeY;

        public int PlayerGameFieldMaxSizeX => _playerGameFieldMaxSizeX;
        public int PlayerGameFieldMaxSizeY => _playerGameFieldMaxSizeY;

        public float CellOffset => _cellOffset;
        public float CellHight => _cellHight;
    }
}
