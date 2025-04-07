using UnityEngine;
using UnityEngine.UI;

namespace Kdevaulo.WordPuzzle.Presentation.Views
{
    [AddComponentMenu(nameof(CellView) + " in " + nameof(Views))]
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
            return _transform.anchoredPosition;
        }
    }
}