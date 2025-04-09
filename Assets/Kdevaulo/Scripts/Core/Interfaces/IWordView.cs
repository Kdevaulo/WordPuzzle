using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IWordView
    {
        public void SetCellState(int cellIndex, State state);
        public AnchorPreset GetAnchorPreset();
        public ITransform GetTransform();
    }
}