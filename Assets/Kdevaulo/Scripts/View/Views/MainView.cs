using Kdevaulo.WordPuzzle.Core;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.View
{
    [AddComponentMenu(nameof(MainView) + " in " + nameof(View))]
    public class MainView : MonoBehaviour, IScaleProvider
    {
        [field: SerializeField] public Transform ClustersParent { get; private set; }
        [field: SerializeField] public Canvas DragCanvas { get; private set; }

        float IScaleProvider.GetScaleFactor()
        {
            return DragCanvas.scaleFactor;
        }
    }
}