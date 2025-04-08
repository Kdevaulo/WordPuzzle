using System.Numerics;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IClusterView
    {
        public void SetAnchoredPosition(Vector2 targetPosition);
        public Vector2 GetAnchoredPosition();
        public void Initialize(ITransform transform, string text);
    }
}