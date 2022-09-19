using System.Collections.Generic;
using UnityEngine;

using CubeDefender.Locator;
using CubeDefender.Service;
using CubeDefender.Chip;

using Random = UnityEngine.Random;

namespace CubeDefender.Builder
{
    public struct CreateChipBuilder : IBuilder
    {
        private int _createdLineIndex;
        private int _chipCount;
        private float _createKeyChance;
        private float _createBonusChance;
        private bool _isCreateKey;
        private bool _isCreateBonus;

        public CreateChipBuilder WhereCreatedLineIndex(int lineIndex)
        {
            _createdLineIndex = lineIndex;

            return this;
        }

        public CreateChipBuilder WhereCreateKeyStatus(bool status)
        {
            _isCreateKey = status;

            return this;
        }

        public CreateChipBuilder WhereCreateBonusStatus(bool status)
        {
            _isCreateBonus = status;

            return this;
        }

        public void Build()
        {
            var enemyFieldService = ServiceLocator.Get<EnemyGameFieldService>();
            var playerService = ServiceLocator.Get<PlayerService>();

            _chipCount = Mathf.Min(
                playerService.EnemyStartCount + playerService.EnemyCountModifre,
                enemyFieldService.MaxX);
            playerService.AddEnemyWave();

            _createBonusChance = playerService.CreateBonusChance / 100;
            _createKeyChance = playerService.CreateKeyChance / 100;

            CreateChips(GetChipsRandomIndex(enemyFieldService.MaxX));
        }

        private HashSet<int> GetChipsRandomIndex(int maxValue)
        {
            var randomCellIndex = new HashSet<int>();

            if (_chipCount > 2)
                _chipCount = Random.Range(1, _chipCount);

            for (var i = 0; i < _chipCount; ++i)
            {
                var randomIndex = Random.Range(default, maxValue);

                if (randomCellIndex.Contains(randomIndex))
                {
                    while (randomCellIndex.Contains(randomIndex))
                    {
                        randomIndex++;

                        if (randomIndex >= maxValue)
                            randomIndex = default;
                    }
                }

                randomCellIndex.Add(randomIndex);
            }

            return randomCellIndex;
        }

        private void CreateChips(HashSet<int> chipIndex)
        {
            var chipService = ServiceLocator.Get<EnemyChipService>();

            foreach(var index in chipIndex)
            {                
                if (_isCreateKey & Random.value <= _createKeyChance)
                {
                    Create(EnemyChipType.Key, index, _createdLineIndex);
                    _isCreateKey = false;
                    continue;
                }

                if(_isCreateBonus & Random.value <= _createBonusChance)
                {
                    Create(EnemyChipType.Bomb, index, _createdLineIndex);
                    _isCreateBonus = false;
                    continue;
                }

                Create(EnemyChipType.Enemy, index, _createdLineIndex);
            }

            void Create(EnemyChipType chipType, int cellIndex, int lineIndex) => chipService.Add(chipType, cellIndex, lineIndex);
        }
    }
}
