using UnityEngine;

using CubeDefender.Service;
using CubeDefender.Locator;

namespace CubeDefender.Builder
{
    public struct GameFieldVisualDebugBuilder<T> : IBuilder
        where T : GameFieldService
    {
        public void Build()
        {
            var service = ServiceLocator.Get<T>();
            var container = new GameObject("EnemyTileContainer");

            for(var x = 0; x < service.MaxX; ++x)
            {
                for(var y = 0; y < service.MaxY; ++y)
                {
                    var debugVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    var position = service.GetCellPosition(x, y);
                    position.y = default;
                    debugVisual.transform.position = position;
                    debugVisual.transform.localScale = Vector3.one * 2;
                    debugVisual.transform.SetParent(container.transform);
                }
            }
        }
    }
}
