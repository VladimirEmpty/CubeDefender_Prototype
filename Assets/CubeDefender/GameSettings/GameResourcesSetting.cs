using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using CubeDefender.Chip;
using CubeDefender.GameFieldTile;

namespace CubeDefender.GameSettings
{
    [CreateAssetMenu(fileName = nameof(GameResourcesSetting), menuName = "CubeDefender/Settings/GameResources")]
    public sealed class GameResourcesSetting : ScriptableObject
    {
        [SerializeField] private List<PlayerChipPrefab> _playerPrefabs;
        [SerializeField] private List<EnemyChipPrefab> _enemyChipPrefabs;
        [SerializeField] private PlayerTile _playerTilePrefab;
        [SerializeField] private Rigidbody _projectile;
        [SerializeField] private EnemyChip _enemyChipDebugPrefab;
        [SerializeField] private PlayerChip _playerChipDebugPrefab;

        public Rigidbody Projectile => _projectile;
        public PlayerTile PlayerTile => _playerTilePrefab;

        public PlayerChip GetPlayerChipPrefab(PlayerChipRank rank)
        {
            var playerPrefab = _playerPrefabs.FirstOrDefault(x => x.rank == rank);

            return playerPrefab.prefab ?? _playerChipDebugPrefab;
        }

        public EnemyChip GetEnemyChipPrefab(EnemyChipType type)
        {
            var enemyPrefab = _enemyChipPrefabs.FirstOrDefault(x => x.type == type);

            return enemyPrefab.prefab ?? _enemyChipDebugPrefab;
        }

        [Serializable]
        private struct PlayerChipPrefab
        {
            public PlayerChipRank rank;
            public PlayerChip prefab;
        }

        [Serializable]
        private struct EnemyChipPrefab
        {
            public EnemyChipType type;
            public EnemyChip prefab;
        }
    }
}
