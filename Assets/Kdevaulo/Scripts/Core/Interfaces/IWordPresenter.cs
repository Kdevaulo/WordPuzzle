using System.Numerics;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IWordPresenter
    {
        void InitializeWord(IWordView view, Vector2[] cellPositions);
        void TryOccupyCells(IWordView view);
        void SetIsPointerOver(IWordView view, bool value);
    }
}