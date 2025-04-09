using System.Numerics;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IWordPresenter
    {
        public void InitializeWord(IWordView view, Vector2[] cellPositions);
        public void TryOccupyCells(IWordView view);
        public void SetIsPointerOver(IWordView view, bool value);
    }
}