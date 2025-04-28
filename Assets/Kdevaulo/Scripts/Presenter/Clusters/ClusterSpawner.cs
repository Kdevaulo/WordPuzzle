using System.Collections.Generic;
using System.Linq;

using Kdevaulo.WordPuzzle.Model;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class ClusterSpawner
    {
        private readonly ClusterView[] _clusterViewPrefabs;

        private readonly ClustersPresenter _clustersPresenter;

        private readonly DiContainer _container;
        private readonly MainGameView _mainView;
        private readonly Transform _parent;

        private List<ClusterView> _createdViews;

        public ClusterSpawner(ClusterView[] clusterViewPrefabs, DiContainer container, MainGameView mainView,
            ClustersPresenter clustersPresenter)
        {
            _clusterViewPrefabs = clusterViewPrefabs;
            _clustersPresenter = clustersPresenter;
            _container = container;
            _mainView = mainView;
            _parent = mainView.ClustersParent;
            _createdViews = new List<ClusterView>();
        }

        public void CreateClusters(Level level)
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

            _clustersPresenter.AddCluster(createdItem, text);
        }
    }
}