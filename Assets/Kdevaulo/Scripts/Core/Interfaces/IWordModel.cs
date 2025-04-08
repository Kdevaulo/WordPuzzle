using System.Numerics;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IWordModel
    {
        public void TryHighlightClosest(Vector2 draggingViewPosition, int count, IWordView view);
        void SetCells(IWordView view, Vector2[] cellPositions);
        void ClearSelectedCells();
        Cell[] GetSelectedCells(IWordView view);
        void OccupyCells(Cell[] selectedCells, Cluster cluster);
        void TryFreeCells(Cluster cluster);
        void SetIsPointerOver(IWordView wordView, bool value);
        void ResetPointerOver();
        IWordView GetSelectedWordView();
    }
}