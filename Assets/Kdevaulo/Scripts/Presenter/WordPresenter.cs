using System;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using Zenject;

using Vector2 = System.Numerics.Vector2;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class WordPresenter : IWordPresenter, ITickable
    {
        [Inject]
        private IWordModel _wordModel;
        [Inject]
        private DragHandler _dragHandler;

        void ITickable.Tick()
        {
            _wordModel.ClearSelected();

            var draggingItem = _dragHandler.CurrentItem;

            if (draggingItem != null)
            {
                var views = _wordModel.GetSelectedWordViews();

                var position = draggingItem.GetPosition();

                foreach (var view in views)
                {
                    var cellsCount = draggingItem.ClusterLength;

                    TryHighlightCells(view, position, cellsCount);
                }
            }
        }

        void IWordPresenter.InitializeWord(IWordView view, Vector2[] cellPositions)
        {
            _wordModel.SetCells(view, cellPositions);
        }

        void IWordPresenter.TryOccupyCells(IWordView view)
        {
            var selectedCells = _wordModel.GetSelectedCells(view);

            if (selectedCells.Length == 0)
            {
                _wordModel.TryFreeCells(view);
                return;
            }

            _wordModel.OccupyCells(selectedCells);

            var position = CalculateTargetPosition(selectedCells);

            _dragHandler.HandleCorrectDrop(position, view.GetAnchorPreset(), view.GetTransform());
        }

        void IWordPresenter.SetIsPointerOver(IWordView wordView, bool value)
        {
            _wordModel.SetIsPointerOver(wordView, value);
        }

        private void TryHighlightCells(IWordView view, Vector2 draggingViewPosition, int highlightCount)
        {
            _wordModel.TryHighlightClosest(draggingViewPosition, highlightCount, view);
        }

        private Vector2 CalculateTargetPosition(Cell[] cells)
        {
            var count = cells.Length;

            Vector2 position;

            if (count % 2 == 0)
            {
                var upBorderIndex = count / 2;
                var downBorderIndex = upBorderIndex - 1;
                var firstCell = cells[downBorderIndex];
                var secondCell = cells[upBorderIndex];

                position = (firstCell.Position + secondCell.Position) / 2f;
            }
            else
            {
                var flooredHalf = (int) Math.Floor(count / 2f);
                position = cells[flooredHalf].Position;
            }

            return position;
        }
    }
}