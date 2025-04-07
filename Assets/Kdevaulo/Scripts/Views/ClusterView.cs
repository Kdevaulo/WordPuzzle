using TMPro;

using UnityEngine;

using Assert = UnityEngine.Assertions.Assert;

namespace Kdevaulo.WordPuzzle.Views
{
    [AddComponentMenu(nameof(ClusterView) + " in " + nameof(Views))]
    public class ClusterView : MonoBehaviour
    {
        public int ClusterLength => _letterContainers.Length;

        [SerializeField] private TextMeshProUGUI[] _letterContainers;

        public void SetClusterText(string cluster)
        {
            var length = cluster.Length;

            Assert.IsTrue(length == _letterContainers.Length);

            for (int i = 0; i < length; i++)
            {
                _letterContainers[i].text = cluster[i].ToString();
            }
        }
    }
}