using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IClusterModel
    {
        public void AddCluster(IClusterView createdItem, string text);
        public Cluster GetCluster(IClusterView view);
    }
}