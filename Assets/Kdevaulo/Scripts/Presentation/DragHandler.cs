using Kdevaulo.WordPuzzle.Presentation.Views;

namespace Kdevaulo.WordPuzzle.Presentation
{
    public class DragHandler
    {
        public ClusterView CurrentDraggingView { get; private set; }

        public void BeginDrag(ClusterView cluster)
        {
            CurrentDraggingView = cluster;
        }

        public void EndDrag(ClusterView cluster)
        {
            if (CurrentDraggingView == cluster)
                CurrentDraggingView = null;
        }
    }
}