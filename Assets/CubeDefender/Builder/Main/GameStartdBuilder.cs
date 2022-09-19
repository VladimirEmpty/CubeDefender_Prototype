using System;

using CubeDefender.Service;
using CubeDefender.GameSettings;

namespace CubeDefender.Builder
{
    public struct GameStartdBuilder : IBuilder
    {
        private CreateGameFieldSetting _createGameFieldSetting;

        public GameStartdBuilder WhereGameFieldSetting(CreateGameFieldSetting createGameFieldSetting)
        {
            _createGameFieldSetting = createGameFieldSetting;

            return this;
        } 

        public void Build()
        {
            if (_createGameFieldSetting == null)
                throw new NullReferenceException("[StartGameBuilder]: Not set CreateGameFieldSetting.");

            new GameFieldBuilder<EnemyGameFieldService>()
                .WhereGameFieldLimit(
                            _createGameFieldSetting.EnemyGameFieldMaxSizeX,
                            _createGameFieldSetting.EnemyGameFieldMaxSizeY)
                .WhereCellOffset(_createGameFieldSetting.CellOffset)
                .WhereCellHight(_createGameFieldSetting.CellHight)
                .WhereGameFieldPivotPoint(_createGameFieldSetting.EnemyGameFieldPivotPoint)
                .Build();

            new GameFieldBuilder<PlayerGameFieldService>()
                .WhereGameFieldLimit(
                            _createGameFieldSetting.PlayerGameFieldMaxSizeX,
                            _createGameFieldSetting.PlayerGameFieldMaxSizeY)
                .WhereCellOffset(_createGameFieldSetting.CellOffset)
                .WhereCellHight(_createGameFieldSetting.CellHight)
                .WhereGameFieldPivotPoint(_createGameFieldSetting.PlayerGameFieldPivotPoint)
                .Build();        
        }
    }
}
