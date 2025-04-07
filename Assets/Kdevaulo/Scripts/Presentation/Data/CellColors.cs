using System;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.Presentation.Data
{
    [Serializable]
    public struct CellColors
    {
        public Color HighlightColor;
        public Color OccupiedColor;
        public Color NormalColor;
    }
}