using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using CubeDefender.GUI.MVC.View;

namespace CubeDefender.GUI.Elements
{
    [RequireComponent(typeof(Image), typeof(CanvasGroup))]
    public sealed class CreatePlayerChipElement : MonoBehaviour, IView, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Color _enableColor;
        [SerializeField] private Color _disableColor;

        public Image Image => _image;
        public CanvasGroup CanvasGroup => _canvasGroup;
        public Color EnableColor => _enableColor;
        public Color DisableColor => _disableColor;

        public event Action OnPlayerStartDrag;
        public event Action OnPlayerEndDrag;

        public void OnBeginDrag(PointerEventData eventData) => OnPlayerStartDrag?.Invoke();

        public void OnDrag(PointerEventData eventData) { }

        public void OnEndDrag(PointerEventData eventData) => OnPlayerEndDrag?.Invoke();
    }
}
