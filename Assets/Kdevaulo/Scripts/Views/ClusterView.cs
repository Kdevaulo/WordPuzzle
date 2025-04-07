using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

using Assert = UnityEngine.Assertions.Assert;

namespace Kdevaulo.WordPuzzle.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu(nameof(ClusterView) + " in " + nameof(Views))]
    public class ClusterView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static ClusterView CurrentDragged { get; private set; }

        public int ClusterLength => _letterContainers.Length;

        [SerializeField] private TextMeshProUGUI[] _letterContainers;

        [SerializeField] private RectTransform _transform;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Canvas _draggableCanvas;

        private Transform _startParent;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            CurrentDragged = this;

            _canvasGroup.blocksRaycasts = false;

            SetParent(_draggableCanvas.transform, true);

            transform.SetAsLastSibling();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            SetPosition(_transform.anchoredPosition + eventData.delta / _draggableCanvas.scaleFactor);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            CurrentDragged = null;
            _canvasGroup.blocksRaycasts = true;

            if (_draggableCanvas.transform == _transform.parent)
            {
                SetParent(_startParent);
            }
        }

        public void Initialize(Canvas draggableCanvas)
        {
            _draggableCanvas = draggableCanvas;
            _startParent = transform.parent;
        }

        public void SetClusterText(string cluster)
        {
            var length = cluster.Length;

            Assert.IsTrue(length == _letterContainers.Length);

            for (var i = 0; i < length; i++)
            {
                _letterContainers[i].text = cluster[i].ToString();
            }
        }

        public void SetPosition(Vector2 targetPosition)
        {
            _transform.anchoredPosition = new Vector3(targetPosition.x, targetPosition.y, 0);
        }

        public void SetParent(Transform parent, bool positionStays = false)
        {
            _transform.SetParent(parent, positionStays);
        }

        public void SetAnchorPreset(Vector2 min, Vector2 max, Vector2 pivot)
        {
            _transform.anchorMin = min;
            _transform.anchorMax = max;
            _transform.pivot = pivot;
        }
    }
}