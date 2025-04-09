using System.Numerics;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IDraggingItem : IWordPart, IClusterView
    {
        public void SetPosition(Vector2 position);
        public void SetAnchorPreset(AnchorPreset preset);
        public void SetParent(ITransform transform);
    }
}