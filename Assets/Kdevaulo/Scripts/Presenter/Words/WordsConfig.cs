using UnityEngine;

namespace Kdevaulo.WordPuzzle.Presenter
{
    [CreateAssetMenu(fileName = nameof(WordsConfig), menuName = nameof(WordPuzzle) + "/" + nameof(WordsConfig))]
    public class WordsConfig : ScriptableObject
    {
        [field: SerializeField] public CellColors Colors { get; private set; }
    }
}