using System.Numerics;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public static class VectorExtensions
    {
        public static Vector2 ToNumerics(this UnityEngine.Vector2 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static UnityEngine.Vector2 ToUnity(this Vector2 vector)
        {
            return new UnityEngine.Vector2(vector.X, vector.Y);
        }
    }
}