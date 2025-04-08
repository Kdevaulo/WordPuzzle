using System.Numerics;

using Kdevaulo.WordPuzzle.Core;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class ClusterPresenter : IClusterPresenter
    {
        [Inject]
        private IScaleProvider _scaleFactorProvider;
        [Inject]
        private IClusterModel _model;
        [Inject]
        private DragHandler _dragHandler;

        void IClusterPresenter.HandleDrag(IClusterView clusterView, Vector2 delta)
        {
            clusterView
                .SetAnchoredPosition(clusterView.GetAnchoredPosition() + delta / _scaleFactorProvider.GetScaleFactor());
        }

        void IClusterPresenter.HandleDrop(IDraggingItem view)
        {
            _dragHandler.RemoveItem();
        }

        void IClusterPresenter.HandleBeginDrag(IDraggingItem view)
        {
            _dragHandler.AddItem(view);
        }
    }
}