using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

using Zenject;

using Assert = UnityEngine.Assertions.Assert;
using Vector2 = System.Numerics.Vector2;

namespace Kdevaulo.WordPuzzle.View
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu(nameof(ClusterView) + " in " + nameof(View))]
    public class ClusterView : MonoBehaviour, IClusterView, IDraggingItem, IBeginDragHandler, IDragHandler,
        IEndDragHandler
    {
        public int ClusterLength => _letterContainers.Length;

        [SerializeField] private TextMeshProUGUI[] _letterContainers;
        [SerializeField] private RectTransform _transform;
        [SerializeField] private CanvasGroup _canvasGroup;

        private IClusterPresenter _clusterPresenter;

        private Canvas _draggableCanvas;
        private Transform _startParent;

        [Inject]
        public void Construct(IClusterPresenter clusterPresenter)
        {
            _clusterPresenter = clusterPresenter;
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
            _clusterPresenter.HandleBeginDrag(this);

            _canvasGroup.blocksRaycasts = false;
            SetParent(_draggableCanvas.transform, true);
            _transform.SetAsLastSibling();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _clusterPresenter.HandleDrag(this, eventData.delta.ToNumerics());
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _clusterPresenter.HandleDrop(this);

            _canvasGroup.blocksRaycasts = true;

            if (_draggableCanvas.transform == _transform.parent)
            {
                SetParent(_startParent);
            }
        }

        Vector2 IClusterItem.GetPosition()
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

        private void SetParent(Transform parent, bool positionStays = false)
        {
            _transform.SetParent(parent, positionStays);
        }
    }
}