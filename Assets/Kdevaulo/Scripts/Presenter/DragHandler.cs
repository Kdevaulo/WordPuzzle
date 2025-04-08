using System;
using System.Numerics;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class DragHandler
    {
        public event Action<Cluster> ClusterWrongDrop;
        public event Action<Cluster> ClusterDragBegin;

        public IWordPart CurrentItem => _draggingItem;
        public Cluster DraggingCluster { get; private set; }

        private IDraggingItem _draggingItem;

        private bool _droppedToCells;

        public void AddItem(IDraggingItem item, Cluster cluster)
        {
            _draggingItem = item;
            DraggingCluster = cluster;
            ClusterDragBegin?.Invoke(cluster);
        }

        public void RemoveItem()
        {
            if (!_droppedToCells)
            {
                ClusterWrongDrop?.Invoke(DraggingCluster);
            }

            _draggingItem = null;
            DraggingCluster = null;
            _droppedToCells = false;
        }

        public void HandleCorrectDrop(Vector2 position, AnchorPreset preset, ITransform transform)
        {
            if (CurrentItem != null)
            {
                _droppedToCells = true;

                _draggingItem.SetParent(transform);
                _draggingItem.SetAnchorPreset(preset);
                _draggingItem.SetPosition(position);
            }
        }
    }
}