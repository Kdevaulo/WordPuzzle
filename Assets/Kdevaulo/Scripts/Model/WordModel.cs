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
        private Dictionary<IWordView, bool> _pointerOverView = new Dictionary<IWordView, bool>();
        private Dictionary<Cell, Action> _cellsSubscriptions = new Dictionary<Cell, Action>();

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

        void IWordModel.ClearSelected()
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

        void IWordModel.OccupyCells(Cell[] selectedCells)
        {
            foreach (var cell in selectedCells)
            {
                cell.CurrentState = State.Occupied;
            }
        }

        void IWordModel.TryFreeCells(IWordView view)
        {
        }

        void IWordModel.SetIsPointerOver(IWordView wordView, bool value)
        {
            _pointerOverView[wordView] = value;
        }

        List<IWordView> IWordModel.GetSelectedWordViews()
        {
            var views = new List<IWordView>();

            foreach (var item in _pointerOverView)
            {
                if (item.Value)
                {
                    views.Add(item.Key);
                }
            }

            return views;
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
    }
}