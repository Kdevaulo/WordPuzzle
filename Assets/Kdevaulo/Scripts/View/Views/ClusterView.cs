using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

using Zenject;

using Assert = UnityEngine.Assertions.Assert;

namespace Kdevaulo.WordPuzzle.View
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu(nameof(ClusterView) + " in " + nameof(View))]
    public class ClusterView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public int ClusterLength => _letterContainers.Length;

        [SerializeField] private TextMeshProUGUI[] _letterContainers;
        [SerializeField] private RectTransform _transform;
        [SerializeField] private CanvasGroup _canvasGroup;

        private DragHandler _dragHandler;
        private Canvas _draggableCanvas;
        private Transform _startParent;

        [Inject]
        public void Construct(DragHandler dragHandler)
        {
            _dragHandler = dragHandler;
        }

        public void Initialize(Canvas draggableCanvas)
        {
            _draggableCanvas = draggableCanvas;
            _startParent = transform.parent;
        }

        public void SetClusterText(string cluster)
        {
            Assert.IsTrue(cluster.Length == _letterContainers.Length);

            for (var i = 0; i < cluster.Length; i++)
            {
                _letterContainers[i].text = cluster[i].ToString();
            }
        }

        public void SetPosition(Vector2 targetPosition)
        {
            _transform.anchoredPosition = targetPosition;
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

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _dragHandler.BeginDrag(this);

            _canvasGroup.blocksRaycasts = false;
            SetParent(_draggableCanvas.transform, true);
            _transform.SetAsLastSibling();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            SetPosition(_transform.anchoredPosition + eventData.delta / _draggableCanvas.scaleFactor);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _dragHandler.EndDrag(this);

            _canvasGroup.blocksRaycasts = true;
            if (_draggableCanvas.transform == _transform.parent)
                SetParent(_startParent);
        }
    }
}