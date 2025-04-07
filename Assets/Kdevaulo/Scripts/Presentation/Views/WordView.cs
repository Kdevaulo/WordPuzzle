using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;

using Zenject;

using CellColors = Kdevaulo.WordPuzzle.Presentation.Data.CellColors;

namespace Kdevaulo.WordPuzzle.Presentation.Views
{
    [AddComponentMenu(nameof(WordView) + " in " + nameof(Views))]
    public class WordView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Values")]
        [SerializeField] private Vector2 _anchorMin = Vector2.one / 2f;
        [SerializeField] private Vector2 _anchorMax = Vector2.one / 2f;
        [SerializeField] private Vector2 _pivot = Vector2.one / 2f;

        [Header("References")]
        [SerializeField] private CellView[] _cells;
        [SerializeField] private RectTransform _transform;

        private readonly Dictionary<CellView, bool> _cellsOccupancy = new Dictionary<CellView, bool>();

        private CellView[] _targetCells;

        private DragHandler _dragHandler;
        private CellColors _cellColors;

        private bool _isPointerOver;

        [Inject]
        public void Construct(DragHandler dragHandler)
        {
            _dragHandler = dragHandler;

            _cellColors = new CellColors
            {
                HighlightColor = Color.green,
                OccupiedColor = Color.yellow,
                NormalColor = Color.white
            };

            _cellsOccupancy.Clear();

            foreach (var cell in _cells)
            {
                _cellsOccupancy[cell] = false;
            }
        }

        private void Update()
        {
            UpdateCellsColors();

            if (_isPointerOver && _dragHandler.CurrentDraggingView != null)
            {
                var pos = _dragHandler.CurrentDraggingView.transform.position;

                var closestSet = _cells
                    .OrderBy(x => Vector2.Distance(x.transform.position, pos))
                    .Take(_dragHandler.CurrentDraggingView.ClusterLength)
                    .ToHashSet();

                _targetCells = _cells
                    .Where(x => closestSet.Contains(x))
                    .ToArray();

                TryHighlightCells(_targetCells);
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            _isPointerOver = true;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _isPointerOver = false;
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            var cluster = _dragHandler.CurrentDraggingView;
            if (!_isPointerOver || cluster == null)
                return;

            if (_targetCells != null)
            {
                var count = _targetCells.Length;

                var position = CalculateTargetPosition(count);

                cluster.SetParent(_transform);
                cluster.SetAnchorPreset(_anchorMin, _anchorMax, _pivot);
                cluster.SetPosition(position);
            }

            UpdateCellsColors();
        }

        private Vector2 CalculateTargetPosition(int count)
        {
            Vector2 position;

            if (count % 2 == 0)
            {
                var upBorderIndex = count / 2;
                var downBorderIndex = upBorderIndex - 1;
                var firstCell = _targetCells[downBorderIndex];
                var secondCell = _targetCells[upBorderIndex];

                position = (firstCell.GetPosition() + secondCell.GetPosition()) / 2f;
            }
            else
            {
                var flooredHalf = Mathf.FloorToInt(count / 2f);
                position = _targetCells[flooredHalf].GetPosition();
            }

            return position;
        }

        private void TryHighlightCells(CellView[] targetCells)
        {
            if (targetCells.Any(cell => _cellsOccupancy[cell]))
            {
                _targetCells = null;
                return;
            }

            foreach (var cell in targetCells)
            {
                cell.SetColor(_cellColors.HighlightColor);
            }
        }

        private void UpdateCellsColors()
        {
            foreach (var cell in _cellsOccupancy)
            {
                cell.Key.SetColor(cell.Value
                    ? _cellColors.OccupiedColor
                    : _cellColors.NormalColor);
            }
        }
    }
}