using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kdevaulo.WordPuzzle.Model;
using Kdevaulo.WordPuzzle.Service;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class WordPresenter : IInitializable, ITickable, IDisposable
    {
        [Inject]
        private WordModel _model;
        [Inject]
        private ValidationSystem _validationService;
        [Inject]
        private DragHandler _dragHandler;

        [Inject]
        private WordView[] _views;

        private readonly Dictionary<WordView, WordActions> _viewSubscriptions = new Dictionary<WordView, WordActions>();
        private readonly Dictionary<WordView, CellModel[]> _cellsByViews = new Dictionary<WordView, CellModel[]>();
        private readonly Dictionary<CellModel, Action> _modelSubscriptions = new Dictionary<CellModel, Action>();

        private WordView _currentSelectedView;

        void IInitializable.Initialize()
        {
            foreach (var view in _views)
            {
                SetCells(view, view.GetPositions());

                Action<bool> pointerOver = isOver => SetIsPointerOver(view, isOver);
                view.OnPointerOverChange += pointerOver;

                Action drop = () => TryOccupyCells(view);
                view.OnDrop += drop;

                var actions = new WordActions(drop, pointerOver);

                _viewSubscriptions[view] = actions;
            }

            _dragHandler.ClusterWrongDrop += TryFreeCells;
            _dragHandler.ClusterDragBegin += TryFreeCells;
        }

        void IDisposable.Dispose()
        {
            _dragHandler.ClusterWrongDrop -= TryFreeCells;
            _dragHandler.ClusterDragBegin -= TryFreeCells;

            foreach (var (key, value) in _modelSubscriptions)
            {
                key.StateChanged -= value;
            }

            _modelSubscriptions.Clear();

            foreach (var (key, value) in _viewSubscriptions)
            {
                key.OnPointerOverChange -= value.PointerOver;
                key.OnDrop -= value.Drop;
            }

            _viewSubscriptions.Clear();
        }

        void ITickable.Tick()
        {
            ClearSelectedCells();

            var draggingItem = _dragHandler.CurrentItem;

            if (draggingItem != null)
            {
                var view = _currentSelectedView;

                var position = draggingItem.GetPosition();

                if (view != null)
                {
                    var cellsCount = draggingItem.ClusterLength;

                    TryHighlightClosest(position, cellsCount, view);
                }
            }
        }

        public List<CellModel[]> GetCells()
        {
            var outer = new List<CellModel[]>(_cellsByViews.Count);

            foreach (var item in _cellsByViews)
            {
                outer.Add(item.Value);
            }

            return outer;
        }

        private void TryHighlightClosest(Vector2 draggingViewPosition, int count, WordView view)
        {
            var cells = _cellsByViews[view];

            var closestCells = cells
                .OrderBy(x => Vector2.Distance(x.Position.ToUnity(), draggingViewPosition))
                .Take(count)
                .ToArray();

            var targetCells = cells
                .Where(x => closestCells.Contains(x))
                .ToArray();

            TrySelectCells(targetCells);
        }

        private void ClearSelectedCells()
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

        private void SetCells(WordView view, Vector2[] cellPositions)
        {
            var count = cellPositions.Length;

            var cells = new List<CellModel>(count);

            for (var i = 0; i < count; i++)
            {
                var cell = new CellModel(i, cellPositions[i].ToNumerics());
                cells.Add(cell);

                SubscribeCell(view, i, cell);
            }

            _cellsByViews[view] = cells.ToArray();
        }

        private void SubscribeCell(WordView view, int index, CellModel cellModel)
        {
            Action stateChanged = () => HandleCellStateChanged(view, index);
            cellModel.StateChanged += stateChanged;

            _modelSubscriptions[cellModel] = stateChanged;
        }

        private void HandleCellStateChanged(WordView view, int id)
        {
            var cells = _cellsByViews[view];
            view.SetCellState(id, cells[id].CurrentState);
        }

        private void TrySelectCells(CellModel[] targetCells)
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

        private void TryOccupyCells(WordView view)
        {
            var selectedCells = _cellsByViews[view].Where(x => x.CurrentState == State.Selected).ToArray();

            var cluster = _dragHandler.DraggingCluster;

            if (selectedCells.Length == 0)
            {
                return;
            }

            _model.OccupyCells(selectedCells, cluster);

            var position = CalculateTargetPosition(selectedCells);

            _currentSelectedView = null;
            _dragHandler.HandleCorrectDrop(position, view.GetAnchorPreset(), view.GetTransform());

            var solvedWord = TryGetSolvedWord(cluster);

            if (solvedWord != string.Empty)
            {
                _validationService.ValidateWord(solvedWord);
            }
        }

        private void SetIsPointerOver(WordView wordView, bool value)
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

        private string TryGetSolvedWord(Cluster cluster)
        {
            foreach (var pair in _cellsByViews)
            {
                var cells = pair.Value;

                if (!IsWordSolved(cluster, cells))
                {
                    continue;
                }

                var sb = new StringBuilder();
                BuildTextFromCells(cells, sb);

                return sb.ToString();
            }

            return string.Empty;
        }

        private void BuildTextFromCells(CellModel[] cells, StringBuilder sb)
        {
            Cluster lastCluster = null;

            foreach (var cell in cells)
            {
                var cluster = cell.Cluster;

                if (cluster != lastCluster)
                {
                    sb.Append(cluster.Name);
                    lastCluster = cluster;
                }
            }
        }

        private bool IsWordSolved(Cluster cluster, CellModel[] cells)
        {
            var hasCluster = false;

            foreach (var cell in cells)
            {
                if (cell.CurrentState != State.Occupied)
                {
                    return false;
                }

                if (cell.Cluster == cluster)
                {
                    hasCluster = true;
                }
            }

            return hasCluster;
        }

        private void TryFreeCells(Cluster cluster)
        {
            _currentSelectedView = null;
            _model.TryFreeCells(cluster);
        }

        private Vector2 CalculateTargetPosition(CellModel[] cells)
        {
            var count = cells.Length;

            System.Numerics.Vector2 position;

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

            return position.ToUnity();
        }
    }

    public class WordActions
    {
        public readonly Action<bool> PointerOver;
        public readonly Action Drop;

        public WordActions(Action drop, Action<bool> pointerOver)
        {
            PointerOver = pointerOver;
            Drop = drop;
        }
    }
}