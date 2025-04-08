using System.Numerics;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class DragHandler
    {
        public IClusterItem CurrentItem => _draggingItem;

        private IDraggingItem _draggingItem;

        public void AddItem(IDraggingItem item)
        {
            _draggingItem = item;
        }

        public void RemoveItem()
        {
            _draggingItem = null;
        }

        public void HandleCorrectDrop(Vector2 position, AnchorPreset preset, ITransform transform)
        {
            if (CurrentItem != null)
            {
                _draggingItem.SetParent(transform);
                _draggingItem.SetAnchorPreset(preset);
                _draggingItem.SetPosition(position);
            }
        }
    }
}