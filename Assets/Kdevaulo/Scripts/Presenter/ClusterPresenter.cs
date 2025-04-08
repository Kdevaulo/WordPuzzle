using System.Numerics;

using Kdevaulo.WordPuzzle.Core;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class ClusterPresenter : IClusterPresenter
    {
        [Inject]
        private ICanvasParamsProvider _canvasProvider;
        [Inject]
        private IClusterModel _model;
        [Inject]
        private DragHandler _dragHandler;

        void IClusterPresenter.HandleDrag(IClusterView clusterView, Vector2 delta)
        {
            clusterView
                .SetAnchoredPosition(clusterView.GetAnchoredPosition() + delta / _canvasProvider.GetScaleFactor());
        }

        void IClusterPresenter.HandleDrop(IDraggingItem view)
        {
            _dragHandler.RemoveItem();
        }

        void IClusterPresenter.HandleBeginDrag(IDraggingItem view)
        {
            var cluster = _model.GetCluster(view);
            _dragHandler.AddItem(view, cluster);
        }

        void IClusterPresenter.AddCluster(IClusterView createdItem, string text)
        {
            _model.AddCluster(createdItem, text);

            createdItem.Initialize(_canvasProvider.GetTransform(), text);
        }
    }
}