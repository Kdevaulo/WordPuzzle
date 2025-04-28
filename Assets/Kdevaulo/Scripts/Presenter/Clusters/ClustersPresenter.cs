using System;
using System.Collections.Generic;

using Kdevaulo.WordPuzzle.Model;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class ClustersPresenter : IDisposable
    {
        [Inject]
        private MainGameView _gameView;
        [Inject]
        private DragHandler _dragHandler;

        private Dictionary<ClusterView, DragActions> _dragActions = new Dictionary<ClusterView, DragActions>();
        private Dictionary<ClusterView, Cluster> _clusters = new Dictionary<ClusterView, Cluster>();

        void IDisposable.Dispose()
        {
            foreach (var (key, value) in _dragActions)
            {
                key.OnDrag -= value.Drag;
                key.OnDragBegin -= value.DragBegin;
                key.OnDrop -= RemoveDraggableItem;
            }

            _dragActions.Clear();
        }

        public void AddCluster(ClusterView createdItem, string text)
        {
            var cluster = new Cluster(text);
            _clusters[createdItem] = cluster;

            createdItem.Initialize(_gameView.GetTransform(), text);

            Action<Vector2> dragAction = eventData => MoveCluster(createdItem, eventData);
            createdItem.OnDrag += dragAction;

            Action dragBeginAction = () => AddDraggableItem(createdItem);
            createdItem.OnDragBegin += dragBeginAction;

            createdItem.OnDrop += RemoveDraggableItem;

            _dragActions[createdItem] = new DragActions(dragBeginAction, dragAction);
        }

        private void RemoveDraggableItem()
        {
            _dragHandler.RemoveItem();
        }

        private void AddDraggableItem(ClusterView view)
        {
            var cluster = _clusters[view];
            _dragHandler.AddItem(view, cluster);
        }

        private void MoveCluster(ClusterView clusterView, Vector2 delta)
        {
            clusterView.Move(delta / _gameView.GetScaleFactor());
        }
    }

    public class DragActions
    {
        public readonly Action DragBegin;
        public readonly Action<Vector2> Drag;

        public DragActions(Action dragBegin, Action<Vector2> drag)
        {
            DragBegin = dragBegin;
            Drag = drag;
        }
    }
}