using Kdevaulo.WordPuzzle.Presentation.Data;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.Data
{
    [CreateAssetMenu(fileName = nameof(WordsData), menuName = nameof(WordPuzzle) + "/" + nameof(WordsData))]
    public class WordsData : ScriptableObject
    {
        [field: SerializeField] public CellColors[] Colors { get; private set; }
    }
}