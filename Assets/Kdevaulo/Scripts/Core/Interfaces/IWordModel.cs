using System.Numerics;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IWordModel
    {
        public void TryHighlightClosest(Vector2 draggingViewPosition, int count, IWordView view);
        public void SetCells(IWordView view, Vector2[] cellPositions);
        public void ClearSelectedCells();
        public Cell[] GetSelectedCells(IWordView view);
        public void OccupyCells(Cell[] selectedCells, Cluster cluster);
        public void TryFreeCells(Cluster cluster);
        public void SetIsPointerOver(IWordView wordView, bool value);
        public void ResetPointerOver();
        public IWordView GetSelectedWordView();
    }
}