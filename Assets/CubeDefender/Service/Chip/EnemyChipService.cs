using System;
using System.Linq;
using System.Collections.Generic;

using CubeDefender.Locator;
using CubeDefender.Chip;
using CubeDefender.Pool;
using CubeDefender.StateMachine;
using CubeDefender.StateMachine.State.EnemyChipState;

using Random = UnityEngine.Random;

namespace CubeDefender.Service
{
    /// <summary>
    /// Класс-сервис с логикой по работе с объектами <see cref="EnemyChip"/>
    /// </summary>
    public sealed class EnemyChipService : IService
    {
        private const float MovementSpeed = 5f;

        private readonly EnemyGameFieldService EnemyGameFieldService;
        private readonly EnemyChipPool EnemyChipPool;
        private readonly PlayerService PlayerService;
                
        private IDictionary<EnemyChip, Data> _chipDataBank;
        private Data _targetData;

        public EnemyChipService()
        {
            EnemyGameFieldService = ServiceLocator.Get<EnemyGameFieldService>();
            EnemyChipPool = ServiceLocator.Get<PoolService>().Get<EnemyChipPool>();
            PlayerService = ServiceLocator.Get<PlayerService>();
                        
            _chipDataBank = new Dictionary<EnemyChip, Data>();
        }

        public int OnMovementChipCount { get; private set; }

        private Action<int, int> OnGetLinkedData;

        public void Add(EnemyChipType type, int x, int y)
        {            
            var createdPoint = EnemyGameFieldService.GetCellPosition(x, y);
            var createdChip = EnemyChipPool.Spawn(type, createdPoint);
            var health = type == EnemyChipType.Enemy ? CalculateHealth() : default;

            //DEBUG
            if(type == EnemyChipType.Enemy)
            {
                createdChip.ValueInfo.text = health.ToString();
            }           

            var data = new Data(createdChip, SetTargetData)
            {
                health = health,
                positionX = x,
                positionY = y
            };
            OnGetLinkedData += data.ApplayTargetData;            

            _chipDataBank.Add(createdChip, data);
        }

        public void ExecuteMove()
        {
            var keys = _chipDataBank.Keys.ToArray();
            var chips = new LinkedList<EnemyChip>();
            var isMoved = true;

            for(var i = 0; i < keys.Length; ++i)
            {
                if(++_chipDataBank[keys[i]].positionY >= EnemyGameFieldService.MaxY)
                {
                    switch (_chipDataBank[keys[i]].Chip.Type)
                    {
                        case EnemyChipType.Enemy:
                            GameProcessor.Instance.GameOver();
                            chips.Clear();
                            isMoved = false;
                            break;
                        case EnemyChipType.Bomb:
                        case EnemyChipType.Key:
                            RemoveData(_chipDataBank[keys[i]]);
                            break;
                    }

                    if (!isMoved)
                        break;
                }
                else
                {
                    chips.AddFirst(_chipDataBank[keys[i]].Chip);
                }
            }

            if (chips.Count <= 0) return;

            var movementChip = chips.First;
            OnMovementChipCount = chips.Count;

            while(movementChip != null)
            {
                var data = _chipDataBank[movementChip.Value];
                movementChip.Value.ChangeState<EnemyChip, MovementState>(state =>
                {
                    state.movementPosition = EnemyGameFieldService.GetCellPosition(data.positionX, data.positionY);
                    state.movementSpeed = MovementSpeed;
                    state.OnDecreaseCount += DecreaseEnemyMovementCount;
                });

                movementChip = movementChip.Next;
            }
        }

        public void ExecuteHit(EnemyChip targetChip, int damage)
        {
            if(_chipDataBank.TryGetValue(targetChip, out var data))
            {
                data.health -= damage;

                //DEBUG
                if(targetChip.Type == EnemyChipType.Enemy)
                {
                    targetChip.ValueInfo.text = data.health <= 0
                                                            ? string.Empty
                                                            : data.health.ToString();
                }

                if (data.health > 0) return;

                var posX = data.positionX;
                var posY = data.positionY;

                RemoveData(data);

                switch (targetChip.Type)
                {
                    case EnemyChipType.Bomb:
                        MakeBoom(posX, posY);
                        break;
                    case EnemyChipType.Key:
                        PlayerService.IsContainKey = true;
                        break;
                    case EnemyChipType.Enemy:
                        PlayerService.PlayerScore++;
                        break;
                }                
            }
        }

        public void Clear()
        {
            foreach(var el in _chipDataBank)
            {
                RemoveData(el.Value, false);
            }

            _chipDataBank.Clear();
        }

        private void MakeBoom(int pivotX, int pivotY)
        {
            for(var x = -1; x <= 1; ++x)
            {
                for(var y = -1; y <= 1; ++y)
                {
                    _targetData = null;
                    OnGetLinkedData?.Invoke(pivotX + x, pivotY + y);

                    if (_targetData != null)
                        ExecuteHit(_targetData.Chip, _targetData.health);
                }
            }
        }

        private void RemoveData(Data data, bool isRemovedInBank = true)
        {
            if (isRemovedInBank)
            {
                _chipDataBank.Remove(data.Chip);
            }

            OnGetLinkedData -= data.ApplayTargetData;
            EnemyChipPool.Despawn(data.Chip.Type, data.Chip);
            data.Dispose();
        }

        private int CalculateHealth() => Random.Range(PlayerService.MaxPlayerDamage + PlayerService.EnemyHealthModifire, PlayerService.MaxPlayerDamage * PlayerService.EnemyHealthModifire);
        private void SetTargetData(Data targetData) => _targetData = targetData;
        private void DecreaseEnemyMovementCount() => OnMovementChipCount--;

        private sealed class Data : IDisposable
        {
            public int health;

            public int positionX;
            public int positionY;

            private bool _isDisposed;

            public Data(EnemyChip chip, Action<Data> extructDataCallback)
            {
                Chip = chip;
                ExcturctDataCallback = extructDataCallback;
            }

            public EnemyChip Chip { get; private set; }

            private Action<Data> ExcturctDataCallback;

            public void ApplayTargetData(int x, int y)
            {
                if (positionX == x & positionY == y)
                    ExcturctDataCallback?.Invoke(this);
            }

            public void Dispose()
            {
                if (_isDisposed) return;

                Chip = null;
                _isDisposed = true;

                GC.SuppressFinalize(this);
            }
        }
    }
}
