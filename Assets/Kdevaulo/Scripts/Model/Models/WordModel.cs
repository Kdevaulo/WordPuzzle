using System.Collections.Generic;

namespace Kdevaulo.WordPuzzle.Model
{
    public class WordModel
    {
        private Dictionary<Cluster, CellModel[]> _occupiedCells = new Dictionary<Cluster, CellModel[]>();

        public void OccupyCells(CellModel[] selectedCells, Cluster cluster)
        {
            foreach (var cell in selectedCells)
            {
                cell.Occupy(cluster);
            }

            _occupiedCells[cluster] = selectedCells;
        }

        public void TryFreeCells(Cluster cluster)
        {
            if (!_occupiedCells.TryGetValue(cluster, out var cells))
            {
                return;
            }

            foreach (var cell in cells)
            {
                if (cell.Cluster == cluster)
                {
                    cell.Free();
                }
            }

            _occupiedCells.Remove(cluster);
        }
    }
}