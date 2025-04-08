using System.Numerics;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IWordView
    {
        public void SetCellState(int cellIndex, State state);
        public void HandleCellsOccupied(Vector2 middlePosition);
    }
}