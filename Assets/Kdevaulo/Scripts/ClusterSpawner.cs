using System.Collections.Generic;
using System.Linq;

using Kdevaulo.WordPuzzle.Data;
using Kdevaulo.WordPuzzle.Views;

using UnityEngine;
using UnityEngine.Assertions;

namespace Kdevaulo.WordPuzzle
{
    public class ClusterSpawner
    {
        private readonly ClusterView[] _clusterViews;
        private readonly Transform _parent;

        private List<ClusterView> _createdViews;

        public ClusterSpawner(ClusterView[] clusterViews, MainView mainView)
        {
            _clusterViews = clusterViews;
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
            var targetCluster = _clusterViews.FirstOrDefault(x => x.ClusterLength == length);

            Assert.IsNotNull(targetCluster, $"There is no cluster view with length == {length}");

            var createdItem = Object.Instantiate(targetCluster, _parent);
            _createdViews.Add(createdItem);

            createdItem.SetClusterText(text);
        }
    }
}