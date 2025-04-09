using Kdevaulo.WordPuzzle.Core;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.View
{
    [AddComponentMenu(nameof(MainGameView) + " in " + nameof(View))]
    public class MainGameView : MonoBehaviour,
        ICanvasParamsProvider
    {
        [field: SerializeField] public Transform ClustersParent { get; private set; }
        [field: SerializeField] public Canvas DragCanvas { get; private set; }

        private TransformAdapter _adapter;

        float ICanvasParamsProvider.GetScaleFactor()
        {
            return DragCanvas.scaleFactor;
        }

        ITransform ICanvasParamsProvider.GetTransform()
        {
            return _adapter ?? new TransformAdapter(DragCanvas.transform);
        }
    }
}