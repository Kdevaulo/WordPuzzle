using Kdevaulo.WordPuzzle.Presentation.Views;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.Presentation.Data
{
    [CreateAssetMenu(fileName = nameof(ClustersData), menuName = nameof(WordPuzzle) + "/" + nameof(ClustersData))]
    public class ClustersData : ScriptableObject
    {
        [field: SerializeField] public ClusterView[] Clusters { get; private set; }
    }
}