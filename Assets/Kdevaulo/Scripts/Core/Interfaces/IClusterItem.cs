using System.Numerics;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IClusterItem
    {
        public int ClusterLength { get; }
        public Vector2 GetPosition();
    }
}