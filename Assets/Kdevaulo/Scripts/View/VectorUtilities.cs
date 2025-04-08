using System.Numerics;

using Vector3 = UnityEngine.Vector3;

namespace Kdevaulo.WordPuzzle.View
{
    public static class VectorUtilities
    {
        public static Vector2 ToNumerics(this UnityEngine.Vector2 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static UnityEngine.Vector2 ToUnity(this Vector2 vector)
        {
            return new UnityEngine.Vector2(vector.X, vector.Y);
        }

        public static Vector2 ToNumerics(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
    }
}