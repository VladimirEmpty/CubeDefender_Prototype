using System;
using System.Collections.Generic;
using UnityEngine;

using CubeDefender.Locator;
using CubeDefender.Chip;
using CubeDefender.Pool;

namespace CubeDefender.Service
{
    /// <summary>
    /// Класс-сервис с логикой по работе с объектами <see cref="PlayerChip"/>
    /// </summary>
    public sealed class PlayerChipService : IService
    {
        private readonly PlayerGameFieldService PlayerGameFieldService;
        private readonly ProjectileService ProjectileService;
        private readonly PlayerChipPool PlayerChipPool;
        private readonly PlayerService PlayerService;

        private IDictionary<int, Data> _playerChipMap;

        public PlayerChipService()
        {
            PlayerGameFieldService = ServiceLocator.Get<PlayerGameFieldService>();
            ProjectileService = ServiceLocator.Get<ProjectileService>();
            PlayerChipPool = ServiceLocator.Get<PoolService>().Get<PlayerChipPool>();
            PlayerService = ServiceLocator.Get<PlayerService>();

            _playerChipMap = new Dictionary<int, Data>();
        }

        public int MaxPlayerDamage { get; private set; }

        public void ExecuteAddChip(int x, int y, PlayerChipRank rank)
        {
            if (!ValidateInputPosition(x, y)) return;

            if(_playerChipMap.TryGetValue(ConvertToHash(x,y), out var data))
            {
                if (data.rank == rank)
                    MergeChip(data);
                else
                    return;
            }
            else
            {
                var createPosition = PlayerGameFieldService.GetCellPosition(x, y);

                data = new Data();
                data.chip = PlayerChipPool.Spawn(rank, createPosition);
                data.rank = rank;

                _playerChipMap.Add(ConvertToHash(x, y), data);
            }

            var chipDamage = ConvertRankToDamage(data.rank);

            if (MaxPlayerDamage < chipDamage)
                MaxPlayerDamage = chipDamage;

            data.chip.ValueInfo.text = chipDamage.ToString();

            PlayerService.IsContainKey = false;
            GameProcessor.Instance.Execute();
        }

        public void ExecuteMoveChip((int x, int y) from, (int x, int y) to)
        {
            if (!ValidateInputPosition(from.x, from.y) || !ValidateInputPosition(to.x, to.y)) 
                return;

            var fromHash = ConvertToHash(from.x, from.y);
            var toHash = ConvertToHash(to.x, to.y);

            if (!_playerChipMap.TryGetValue(fromHash, out var fromData))
                return;

            if(_playerChipMap.TryGetValue(toHash, out var toData))
            {
                if (fromData.Equals(toData))
                    return;

                if(toData.rank != fromData.rank)
                {
                    FlipChip(fromData, toData);
                }
                else
                {
                    MergeChip(toData);
                    ReturnChip(fromData);
                    _playerChipMap.Remove(fromHash);

                    var chipDamage = ConvertRankToDamage(toData.rank);

                    if (MaxPlayerDamage < chipDamage)
                        MaxPlayerDamage = chipDamage;

                    toData.chip.ValueInfo.text = chipDamage.ToString();
                }
            }
            else
            {
                var moveToPosition = PlayerGameFieldService.GetCellPosition(to.x, to.y);
                toData = new Data();

                MoveChip(
                    fromData, 
                    toData, 
                    moveToPosition);

                _playerChipMap.Remove(fromHash);
                _playerChipMap.Add(toHash, toData);
            }

            GameProcessor.Instance.Execute();
        }

        public void ExecuteFireChip()
        {
            foreach(var el in _playerChipMap)
            {
                ProjectileService.Create(
                                    el.Value.chip.FirePoint,
                                    ConvertRankToDamage(el.Value.rank));
            }            
        }

        public void Remove(int x, int y)
        {
            if (!ValidateInputPosition(x, y)) return;

            var hash = ConvertToHash(x, y);

            if (_playerChipMap.TryGetValue(hash, out var data))
                ReturnChip(data);

            _playerChipMap.Remove(hash);
        }

        public void Clear()
        {
            foreach (var el in _playerChipMap)
                ReturnChip(el.Value);

            _playerChipMap.Clear();
        }

        public bool ContainChip(int x, int y) => _playerChipMap.ContainsKey(ConvertToHash(x, y));
        public void HideChip(int x, int y) => VisaulChipAction(x, y, false);
        public void ShowChip(int x, int y) => VisaulChipAction(x, y, true);
        private int ConvertToHash(int x, int y) => x * 10 + y;
        private int ConvertRankToDamage(PlayerChipRank rank) => (int)Mathf.Pow(PlayerService.DamageValue, (int)rank);

        private void ReturnChip(Data chipData, bool isDispose = true)
        {
            if (chipData == null) return;

            PlayerChipPool.Despawn(chipData.rank, chipData.chip);

            if (!isDispose) return;

            chipData.Dispose();
        }

        private void VisaulChipAction(int x, int y, bool status)
        {
            if (!ValidateInputPosition(x, y)) return;

            if (_playerChipMap.TryGetValue(ConvertToHash(x, y), out var data))
            {
                data.chip.gameObject.SetActive(status);
            }
        }

        private bool ValidateInputPosition(int x, int y)
        {
            var resultX = x >= PlayerGameFieldService.MaxX | x < 0;
            var resultY = y >= PlayerGameFieldService.MaxY | y < 0;

            return !(resultX | resultY);
        }
         
        private void FlipChip(Data fromData, Data toData)
        {
            var movedChip = toData.chip;
            var movedRank = toData.rank;
            var movedExtraMergModifire = toData.extraMergeModifire;
            var movedPositon = movedChip.transform.position;

            movedChip.transform.position = fromData.chip.transform.position;
            fromData.chip.transform.position = movedPositon;

            toData.chip = fromData.chip;
            toData.rank = fromData.rank;
            toData.extraMergeModifire = fromData.extraMergeModifire;

            fromData.chip = movedChip;
            fromData.rank = movedRank;
            fromData.extraMergeModifire = movedExtraMergModifire;
        }

        private void MoveChip(Data fromData, Data toData, Vector3 toPosition)
        {
            toData.chip = fromData.chip;
            toData.rank = fromData.rank;
            toData.extraMergeModifire = fromData.extraMergeModifire;

            toData.chip.transform.position = toPosition;            

            fromData.Dispose();
        }

        private void MergeChip(Data toData)
        {
            var toPosition = toData.chip.transform.position;            
            ReturnChip(toData, false);

            if (++toData.rank > PlayerChipRank.XV)
            {
                toData.rank = PlayerChipRank.XV;
                toData.extraMergeModifire++;
            }

            toData.chip = PlayerChipPool.Spawn(toData.rank, toPosition);
        }
        

        private sealed class Data : IDisposable
        {
            public PlayerChip chip;
            public PlayerChipRank rank;
            public int extraMergeModifire;

            private bool _isDisposed;

            public void Dispose()
            {
                if (_isDisposed) return;

                chip = null;
                rank = default;
                _isDisposed = true;

                GC.SuppressFinalize(this);
            }
        }
    }
}
