using System.Numerics;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IWordPresenter
    {
        void SetSells(IWordView view, Vector2[] cellPositions);
        void TryHighlightCells(IWordView view, Vector2 draggingViewPosition, int count);
        void TryOccupyCells(IWordView view);
        void ClearSelected(IWordView view);
    }
}