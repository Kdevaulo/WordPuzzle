using System.Numerics;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IClusterPresenter
    {
        public void HandleDrag(IClusterView view, Vector2 delta);
        public void HandleDrop(IDraggingItem view);
        public void HandleBeginDrag(IDraggingItem view);
        public void AddCluster(IClusterView createdItem, string text);
    }
}