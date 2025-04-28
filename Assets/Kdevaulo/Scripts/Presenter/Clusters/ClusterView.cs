using System;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Kdevaulo.WordPuzzle.Presenter
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ClusterView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<Vector2> OnDrag;
        public event Action OnDragBegin;
        public event Action OnDrop;

        public int ClusterLength => _letterContainers.Length;

        [SerializeField] private TextMeshProUGUI[] _letterContainers;
        [SerializeField] private RectTransform _transform;
        [SerializeField] private CanvasGroup _canvasGroup;

        private ClustersPresenter _presenter;

        private Transform _draggableCanvas;
        private Transform _startParent;

        public void Initialize(Transform draggableParent, string text)
        {
            SetText(text);

            _draggableCanvas = draggableParent;
            _startParent = _transform.parent;
        }

        public void Move(Vector2 targetPosition)
        {
            _transform.anchoredPosition += targetPosition;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            OnDragBegin?.Invoke();

            _canvasGroup.blocksRaycasts = false;
            SetParent(_draggableCanvas.transform, true);
            _transform.SetAsLastSibling();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            OnDrag?.Invoke(eventData.delta);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            OnDrop?.Invoke();

            _canvasGroup.blocksRaycasts = true;

            if (_draggableCanvas.transform == _transform.parent)
            {
                SetParent(_startParent);
            }
        }

        public Vector2 GetPosition()
        {
            return _transform.position;
        }

        public void SetPosition(Vector2 position)
        {
            _transform.position = position;
        }

        public void SetAnchorPreset(Anchor preset)
        {
            _transform.anchorMin = preset.AnchorMin;
            _transform.anchorMax = preset.AnchorMax;
            _transform.pivot = preset.Pivot;
        }

        private void SetText(string text)
        {
            Assert.IsTrue(text.Length == _letterContainers.Length);

            for (var i = 0; i < text.Length; i++)
            {
                _letterContainers[i].text = text[i].ToString();
            }
        }

        public void SetParent(Transform parent, bool positionStays = false)
        {
            _transform.SetParent(parent, positionStays);
        }
    }
}