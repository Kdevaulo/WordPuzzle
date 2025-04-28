using System;

using Kdevaulo.WordPuzzle.Model;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class DragHandler
    {
        public event Action<Cluster> ClusterWrongDrop;
        public event Action<Cluster> ClusterDragBegin;

        public ClusterView CurrentItem => _draggingItem;
        public Cluster DraggingCluster { get; private set; }

        private ClusterView _draggingItem;

        private bool _droppedToCells;

        public void AddItem(ClusterView item, Cluster cluster)
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

        public void HandleCorrectDrop(Vector2 position, Anchor preset, Transform transform)
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