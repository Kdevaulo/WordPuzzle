using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Model
{
    public class WordModel : IWordModel, IDisposable
    {
        private Dictionary<IWordView, Cell[]> _cellsByViews = new Dictionary<IWordView, Cell[]>();
        private Dictionary<Cluster, Cell[]> _occupiedCells = new Dictionary<Cluster, Cell[]>();
        private Dictionary<Cell, Action> _cellsSubscriptions = new Dictionary<Cell, Action>();

        private IWordView _currentSelectedView;

        void IDisposable.Dispose()
        {
            foreach (var subscription in _cellsSubscriptions)
            {
                subscription.Key.StateChanged -= subscription.Value;
            }

            _cellsSubscriptions.Clear();
        }

        void IWordModel.TryHighlightClosest(Vector2 draggingViewPosition, int count, IWordView view)
        {
            var cells = _cellsByViews[view];

            var closestCells = cells
                .OrderBy(x => Vector2.Distance(x.Position, draggingViewPosition))
                .Take(count)
                .ToArray();

            var targetCells = cells
                .Where(x => closestCells.Contains(x))
                .ToArray();

            TryHighlightCells(targetCells);
        }

        void IWordModel.ClearSelectedCells()
        {
            foreach (var cellPair in _cellsByViews)
            {
                var cells = cellPair.Value;

                foreach (var cell in cells)
                {
                    if (cell.CurrentState == State.Selected)
                    {
                        cell.CurrentState = State.Free;
                    }
                }
            }
        }

        Cell[] IWordModel.GetSelectedCells(IWordView view)
        {
            return _cellsByViews[view].Where(x => x.CurrentState == State.Selected).ToArray();
        }

        void IWordModel.SetCells(IWordView view, Vector2[] cellPositions)
        {
            var count = cellPositions.Length;

            var cells = new List<Cell>(count);

            for (var i = 0; i < count; i++)
            {
                var cell = new Cell(i, cellPositions[i]);
                cells.Add(cell);

                SubscribeCell(view, i, cell);
            }

            _cellsByViews[view] = cells.ToArray();
        }

        void IWordModel.OccupyCells(Cell[] selectedCells, Cluster cluster)
        {
            foreach (var cell in selectedCells)
            {
                cell.Occupy(cluster);
            }

            _occupiedCells[cluster] = selectedCells;
        }

        void IWordModel.TryFreeCells(Cluster cluster)
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

        void IWordModel.ResetPointerOver()
        {
            _currentSelectedView = null;
        }

        void IWordModel.SetIsPointerOver(IWordView wordView, bool value)
        {
            if (_currentSelectedView == wordView && !value)
            {
                _currentSelectedView = null;
            }

            if (value)
            {
                _currentSelectedView = wordView;
            }
        }

        IWordView IWordModel.GetSelectedWordView()
        {
            return _currentSelectedView;
        }

        private void SubscribeCell(IWordView view, int index, Cell cell)
        {
            Action action = () => HandleCellStateChanged(view, index);
            cell.StateChanged += action;
            _cellsSubscriptions[cell] = action;
        }

        private void HandleCellStateChanged(IWordView view, int id)
        {
            var cells = _cellsByViews[view];
            view.SetCellState(id, cells[id].CurrentState);
        }

        private void TryHighlightCells(Cell[] targetCells)
        {
            if (targetCells.Any(cell => cell.CurrentState == State.Occupied))
            {
                return;
            }

            foreach (var cell in targetCells)
            {
                cell.CurrentState = State.Selected;
            }
        }

        public bool IsWordAssembledCorrectly(Word word, Cell[] columnCells)
        {
            var assembledClusters = new List<Cluster>();
            var i = 0;

            while (i < columnCells.Length)
            {
                var startCell = columnCells[i];
                var cluster = startCell.Cluster;

                if (cluster == null)
                    return false;

                var length = cluster.Length;
                if (i + length > columnCells.Length)
                    return false;

                for (var j = 0; j < length; j++)
                {
                    if (columnCells[i + j].Cluster != cluster)
                        return false;
                }

                assembledClusters.Add(cluster);
                i += length;
            }

            if (assembledClusters.Count != word.Clusters.Length)
                return false;

            for (var k = 0; k < assembledClusters.Count; k++)
            {
                if (assembledClusters[k].Name != word.Clusters[k])
                    return false;
            }

            return true;
        }
    }
}