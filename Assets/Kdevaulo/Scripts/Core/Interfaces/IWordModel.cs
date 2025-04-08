using System.Collections.Generic;
using System.Numerics;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IWordModel
    {
        public void TryHighlightClosest(Vector2 draggingViewPosition, int count, IWordView view);
        void SetCells(IWordView view, Vector2[] cellPositions);
        void ClearSelected();
        Cell[] GetSelectedCells(IWordView view);
        void OccupyCells(Cell[] selectedCells);
        void TryFreeCells(IWordView view);
        void SetIsPointerOver(IWordView wordView, bool value);
        List<IWordView> GetSelectedWordViews();
    }
}