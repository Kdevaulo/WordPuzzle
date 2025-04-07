using UnityEngine;

namespace Kdevaulo.WordPuzzle.Views
{
    [AddComponentMenu(nameof(MainView) + " in " + nameof(Views))]
    public class MainView : MonoBehaviour
    {
        [field: SerializeField] public Transform ClustersParent { get; private set; }
        [field: SerializeField] public Canvas DragCanvas { get; private set; }
    }
}