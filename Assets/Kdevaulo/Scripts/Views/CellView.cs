using UnityEngine;
using UnityEngine.UI;

namespace Kdevaulo.WordPuzzle.Views
{
    [AddComponentMenu(nameof(CellView) + " in " + nameof(Views))]
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Image _background;

        public void SetColor(Color color)
        {
            _background.color = color;
        }
    }
}