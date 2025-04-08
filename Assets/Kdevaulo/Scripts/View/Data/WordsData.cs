using UnityEngine;

namespace Kdevaulo.WordPuzzle.View.Data
{
    [CreateAssetMenu(fileName = nameof(WordsData), menuName = nameof(WordPuzzle) + "/" + nameof(WordsData))]
    public class WordsData : ScriptableObject
    {
        [field: SerializeField] public CellColors Colors { get; private set; }
    }
}