using UnityEngine;

namespace Kdevaulo.WordPuzzle.Presenter
{
    [CreateAssetMenu(fileName = nameof(ClustersConfig), menuName = nameof(WordPuzzle) + "/" + nameof(ClustersConfig))]
    public class ClustersConfig : ScriptableObject
    {
        [field: SerializeField] public ClusterView[] Clusters { get; private set; }
    }
}