using System.Numerics;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IWordPart
    {
        public int ClusterLength { get; }
        public Vector2 GetPosition();
    }
}