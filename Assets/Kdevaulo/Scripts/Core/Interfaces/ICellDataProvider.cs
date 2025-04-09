using System.Collections.Generic;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface ICellDataProvider
    {
        public List<Cell[]> GetCells();
    }
}