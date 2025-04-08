using System.Numerics;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IClusterPresenter
    {
        public void HandleDrag(IClusterView view, Vector2 delta);
        void HandleDrop(IDraggingItem view);
        void HandleBeginDrag(IDraggingItem view);
        void AddCluster(IClusterView createdItem, string text);
    }
}