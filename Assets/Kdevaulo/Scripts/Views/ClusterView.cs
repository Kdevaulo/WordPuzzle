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
        public int ClusterLength => _letterContainers.Length;

        [SerializeField] private TextMeshProUGUI[] _letterContainers;

        [SerializeField] private RectTransform _transform;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Canvas _draggableCanvas;

        private Transform _startParent;
        private Vector2 _startPosition;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = _transform.anchoredPosition;
            _startParent = transform.parent;

            _canvasGroup.blocksRaycasts = false;

            transform.SetParent(_draggableCanvas.transform, worldPositionStays: true);

            transform.SetAsLastSibling();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _transform.anchoredPosition += eventData.delta / _draggableCanvas.scaleFactor;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;

            if (transform.parent == _draggableCanvas.transform)
            {
                transform.SetParent(_startParent, worldPositionStays: false);
                _transform.anchoredPosition = _startPosition;
            }
        }

        public void Initialize(Canvas draggableCanvas)
        {
            _draggableCanvas = draggableCanvas;
        }

        public void SetClusterText(string cluster)
        {
            var length = cluster.Length;

            Assert.IsTrue(length == _letterContainers.Length);

            for (int i = 0; i < length; i++)
            {
                _letterContainers[i].text = cluster[i].ToString();
            }
        }
    }
}