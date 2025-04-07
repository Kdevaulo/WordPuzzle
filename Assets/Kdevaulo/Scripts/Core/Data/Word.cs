using System;

namespace Kdevaulo.WordPuzzle.Core.Data
{
    [Serializable]
    public class Word
    {
        public string Name;
        public string[] Clusters;
    }
}