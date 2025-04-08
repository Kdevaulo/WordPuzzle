using Kdevaulo.WordPuzzle.Core;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.View
{
    public class TransformAdapter : ITransform
    {
        public Transform Transform { get; private set; }

        public TransformAdapter(Transform transform)
        {
            Transform = transform;
        }
    }
}