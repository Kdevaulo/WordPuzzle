using System.Numerics;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IDraggingItem : IClusterItem
    {
        void SetPosition(Vector2 position);
        void SetAnchorPreset(AnchorPreset preset);
        void SetParent(ITransform transform);
    }
}