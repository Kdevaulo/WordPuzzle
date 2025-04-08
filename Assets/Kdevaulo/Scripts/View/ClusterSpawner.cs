using System.Collections.Generic;
using System.Linq;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace Kdevaulo.WordPuzzle.View
{
    public class ClusterSpawner : IClusterSpawner
    {
        private readonly ClusterView[] _clusterViewPrefabs;

        private readonly DiContainer _container;
        private readonly MainView _mainView;
        private readonly Transform _parent;

        private List<ClusterView> _createdViews;

        public ClusterSpawner(ClusterView[] clusterViewPrefabs, DiContainer container, MainView mainView)
        {
            _clusterViewPrefabs = clusterViewPrefabs;
            _mainView = mainView;
            _container = container;
            _parent = mainView.ClustersParent;
            _createdViews = new List<ClusterView>();
        }

        void IClusterSpawner.CreateClusters(Level level)
        {
            var clusters = level.Words.SelectMany(x => x.Clusters);
            var shuffledClusters = clusters.OrderBy(x => Random.value).ToArray();

            foreach (var cluster in shuffledClusters)
            {
                CreateCluster(cluster);
            }
        }

        private void CreateCluster(string text)
        {
            var length = text.Length;
            var targetCluster = _clusterViewPrefabs.FirstOrDefault(x => x.ClusterLength == length);

            Assert.IsNotNull(targetCluster, $"There is no cluster view with length == {length}");

            var createdItem = _container.InstantiatePrefabForComponent<ClusterView>(targetCluster, _parent);
            _createdViews.Add(createdItem);

            createdItem.SetClusterText(text);
            createdItem.Initialize(_mainView.DragCanvas);
        }
    }
}