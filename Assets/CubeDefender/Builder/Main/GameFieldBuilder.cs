using UnityEngine;

using CubeDefender.Locator;
using CubeDefender.Service;
using CubeDefender.Factory;

namespace CubeDefender.Builder
{
    public struct GameFieldBuilder<T> : IBuilder
        where T : GameFieldService
    {
        private Vector3 _pivotPosition;

        private int _limitX;
        private int _limitY;

        private float _cellOffset;
        private float _cellHightOffset;

        public GameFieldBuilder<T> WhereGameFieldLimit(int x, int y)
        {
            _limitX = x;
            _limitY = y;

            return this;
        }

        public GameFieldBuilder<T> WhereCellOffset(float offset)
        {
            _cellOffset = offset;

            return this;
        }

        public GameFieldBuilder<T> WhereCellHight(float hight)
        {
            _cellHightOffset = hight;

            return this;
        }

        public GameFieldBuilder<T> WhereGameFieldPivotPoint(Vector3 position)
        {
            _pivotPosition = position;

            return this;
        }

        public void Build()
        {
            using(var factory = new NativeObjectFactory<T>())
            {
                var service = factory.Create();
                var cellPosition = _pivotPosition;
                cellPosition.y = _cellHightOffset;
                service.Initialize(_limitX, _limitY);

                for (var y = 0; y < _limitY; ++y)
                {
                    for (var x = 0; x < _limitX; ++x)
                    {
                        service.AddCellPosition(x, y, cellPosition);
                        cellPosition.x -= _cellOffset;
                    }

                    cellPosition.z += _cellOffset;
                    cellPosition.x = _pivotPosition.x;
                }

                ServiceLocator.Add(service);
            }
        }
    }
}
