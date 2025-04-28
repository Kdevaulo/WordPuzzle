using UnityEngine;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class MainGameView : MonoBehaviour
    {
        [field: SerializeField] public Transform ClustersParent { get; private set; }
        [field: SerializeField] public Canvas DragCanvas { get; private set; }

        public float GetScaleFactor()
        {
            return DragCanvas.scaleFactor;
        }

        public Transform GetTransform()
        {
            return DragCanvas.transform;
        }
    }
}