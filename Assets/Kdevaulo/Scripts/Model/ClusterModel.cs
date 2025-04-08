using System.Collections.Generic;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Model
{
    public class ClusterModel : IClusterModel
    {
        private Dictionary<IClusterView, Cluster> _clusters = new Dictionary<IClusterView, Cluster>();

        public void AddCluster(IClusterView createdItem, string text)
        {
            _clusters[createdItem] = new Cluster(text);
        }

        public Cluster GetCluster(IClusterView view)
        {
            return _clusters[view];
        }
    }
}