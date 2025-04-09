using System;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using Zenject;

using Vector2 = System.Numerics.Vector2;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class WordPresenter : IInitializable, IWordPresenter, ITickable, IDisposable
    {
        [Inject]
        private IWordModel _model;
        [Inject]
        private IValidationService _validationService;
        [Inject]
        private DragHandler _dragHandler;

        void IInitializable.Initialize()
        {
            _dragHandler.ClusterWrongDrop += TryFreeCells;
            _dragHandler.ClusterDragBegin += TryFreeCells;
        }

        void IDisposable.Dispose()
        {
            _dragHandler.ClusterWrongDrop -= TryFreeCells;
            _dragHandler.ClusterDragBegin -= TryFreeCells;
        }

        void ITickable.Tick()
        {
            _model.ClearSelectedCells();

            var draggingItem = _dragHandler.CurrentItem;

            if (draggingItem != null)
            {
                var view = _model.GetSelectedWordView();

                var position = draggingItem.GetPosition();

                if (view != null)
                {
                    var cellsCount = draggingItem.ClusterLength;

                    TryHighlightCells(view, position, cellsCount);
                }
            }
        }

        void IWordPresenter.InitializeWord(IWordView view, Vector2[] cellPositions)
        {
            _model.SetCells(view, cellPositions);
        }

        void IWordPresenter.TryOccupyCells(IWordView view)
        {
            var selectedCells = _model.GetSelectedCells(view);

            var cluster = _dragHandler.DraggingCluster;

            if (selectedCells.Length == 0)
            {
                return;
            }

            _model.OccupyCells(selectedCells, cluster);

            var position = CalculateTargetPosition(selectedCells);

            _model.ResetPointerOver();
            _dragHandler.HandleCorrectDrop(position, view.GetAnchorPreset(), view.GetTransform());

            var solvedWord = _model.TryGetSolvedWord(cluster);

            if (solvedWord != string.Empty)
            {
                _validationService.ValidateWord(solvedWord);
            }
        }

        void IWordPresenter.SetIsPointerOver(IWordView wordView, bool value)
        {
            _model.SetIsPointerOver(wordView, value);
        }

        private void TryHighlightCells(IWordView view, Vector2 draggingViewPosition, int highlightCount)
        {
            _model.TryHighlightClosest(draggingViewPosition, highlightCount, view);
        }

        private void TryFreeCells(Cluster cluster)
        {
            _model.ResetPointerOver();
            _model.TryFreeCells(cluster);
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