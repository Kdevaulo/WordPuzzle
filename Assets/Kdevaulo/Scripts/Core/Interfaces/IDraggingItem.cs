using System.Numerics;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IDraggingItem : IWordPart, IClusterView
    {
        void SetPosition(Vector2 position);
        void SetAnchorPreset(AnchorPreset preset);
        void SetParent(ITransform transform);
    }
}