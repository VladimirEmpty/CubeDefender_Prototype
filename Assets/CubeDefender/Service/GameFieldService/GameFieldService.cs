using System;
using UnityEngine;

namespace CubeDefender.Service
{
    /// <summary>
    /// Класс-сервис с логикой работы с позициями на игровом поле
    /// </summary>
    public abstract class GameFieldService : IService
    {
        private Vector3[,] _gameFieldCells;

        public GameFieldService()
        {
        }

        public int MaxX { get; private set; }
        public int MaxY { get; private set; }

        public void Initialize(int x, int y)
        {
            _gameFieldCells = new Vector3[x, y];

            MaxX = x;
            MaxY = y;
        }

        public void AddCellPosition(int x, int y, Vector3 cellPosition)
        {
            if (x >= MaxX | y >= MaxY) return;
            if (x < 0 | y < 0) return;

            _gameFieldCells[x, y] = cellPosition;
        }

        public Vector3 GetCellPosition(int x, int y)
        {
            if (x >= MaxX | y >= MaxY)
                throw new ArgumentOutOfRangeException($"[{this.GetType().Name}] Try execute get position with X - {x} and Y - {y}");

            if (x < 0 | y < 0)
                throw new Exception($"[{this.GetType().Name}] Try execute get position with negative value X or Y ");

            return _gameFieldCells[x, y];
        }
    }
}
