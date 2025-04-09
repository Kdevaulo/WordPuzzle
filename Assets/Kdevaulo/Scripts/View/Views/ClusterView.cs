using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

using Zenject;

using Vector2 = System.Numerics.Vector2;

namespace Kdevaulo.WordPuzzle.View
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu(nameof(ClusterView) + " in " + nameof(View))]
    public class ClusterView : MonoBehaviour,
        IDraggingItem, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public int ClusterLength => _letterContainers.Length;

        [SerializeField] private TextMeshProUGUI[] _letterContainers;
        [SerializeField] private RectTransform _transform;
        [SerializeField] private CanvasGroup _canvasGroup;

        private IClusterPresenter _presenter;

        private Transform _draggableCanvas;
        private Transform _startParent;

        [Inject]
        public void Construct(IClusterPresenter presenter)
        {
            _presenter = presenter;
        }

        void IClusterView.Initialize(ITransform transformAdapter, string text)
        {
            var adapter = transformAdapter as TransformAdapter;
            Assert.IsNotNull(adapter);

            SetText(text);

            _draggableCanvas = adapter.Transform;
            _startParent = transform.parent;
        }

        void IClusterView.SetAnchoredPosition(Vector2 targetPosition)
        {
            _transform.anchoredPosition = targetPosition.ToUnity();
        }

        Vector2 IClusterView.GetAnchoredPosition()
        {
            return _transform.anchoredPosition.ToNumerics();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _presenter.HandleBeginDrag(this);

            _canvasGroup.blocksRaycasts = false;
            SetParent(_draggableCanvas.transform, true);
            _transform.SetAsLastSibling();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _presenter.HandleDrag(this, eventData.delta.ToNumerics());
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _presenter.HandleDrop(this);

            _canvasGroup.blocksRaycasts = true;

            if (_draggableCanvas.transform == _transform.parent)
            {
                SetParent(_startParent);
            }
        }

        Vector2 IWordPart.GetPosition()
        {
            return _transform.position.ToNumerics();
        }

        void IDraggingItem.SetPosition(Vector2 position)
        {
            _transform.position = position.ToUnity();
        }

        void IDraggingItem.SetAnchorPreset(AnchorPreset preset)
        {
            _transform.anchorMin = preset.AnchorMin.ToUnity();
            _transform.anchorMax = preset.AnchorMax.ToUnity();
            _transform.pivot = preset.Pivot.ToUnity();
        }

        void IDraggingItem.SetParent(ITransform transformAdapter)
        {
            var adapter = transformAdapter as TransformAdapter;
            Assert.IsNotNull(adapter);

            SetParent(adapter.Transform);
        }

        private void SetText(string text)
        {
            Assert.IsTrue(text.Length == _letterContainers.Length);

            for (var i = 0; i < text.Length; i++)
            {
                _letterContainers[i].text = text[i].ToString();
            }
        }

        private void SetParent(Transform parent, bool positionStays = false)
        {
            _transform.SetParent(parent, positionStays);
        }
    }
}