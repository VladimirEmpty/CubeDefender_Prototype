using System;
using UnityEngine;
using UnityEngine.EventSystems;

using CubeDefender.StateMachine;

namespace CubeDefender.GameFieldTile
{
    /// <summary>
    /// Объект по обработке взаимодействия игрока с полем. Основная обработка происходит в <see cref="Service.PlayerInputService"/>, а настройка в <see cref="Builder.GameFieldPlayerTileBuilder"/>
    /// </summary>
    [RequireComponent(typeof(PlayerTileView), typeof(BoxCollider))]
    public sealed class PlayerTile : MonoBehaviour, IStateMachineOwner, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public PlayerTileView View { get; private set; }
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int Hash => GetHashCode();

        public event Action<PlayerTile> OnStartDragTile;
        public event Action OnEndDragTile;
        public event Action<PlayerTile> OnEnterTile;
        public event Action<PlayerTile> OnExitTile;

        private void Awake()
        {
            View = GetComponent<PlayerTileView>();
        }

        public void Setup(int x, int y)
        {
            PosX = x;
            PosY = y;
        }

        public void OnPointerEnter(PointerEventData eventData) => OnEnterTile?.Invoke(this);

        public void OnPointerExit(PointerEventData eventData) => OnExitTile?.Invoke(this);

        public void OnBeginDrag(PointerEventData eventData) => OnStartDragTile?.Invoke(this);

        public void OnEndDrag(PointerEventData eventData) => OnEndDragTile?.Invoke();

        public void OnDrag(PointerEventData eventData) { }
    }
}
