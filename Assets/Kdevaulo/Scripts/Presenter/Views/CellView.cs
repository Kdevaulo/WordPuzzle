using UnityEngine;
using UnityEngine.UI;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private RectTransform _transform;

        public void SetColor(Color color)
        {
            _background.color = color;
        }

        public Vector2 GetPosition()
        {
            return _transform.position;
        }
    }
}