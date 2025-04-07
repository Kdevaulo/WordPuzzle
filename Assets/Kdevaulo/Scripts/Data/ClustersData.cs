using Kdevaulo.WordPuzzle.Views;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.Data
{
    [CreateAssetMenu(fileName = nameof(ClustersData), menuName = nameof(WordPuzzle) + "/" + nameof(ClustersData))]
    public class ClustersData : ScriptableObject
    {
        [field: SerializeField] public ClusterView[] Clusters { get; private set; }
    }
}