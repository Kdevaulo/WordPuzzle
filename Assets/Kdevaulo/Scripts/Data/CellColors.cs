using System;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.Data
{
    [Serializable]
    public struct CellColors
    {
        public Color HighlightColor;
        public Color OccupiedColor;
        public Color NormalColor;
    }
}