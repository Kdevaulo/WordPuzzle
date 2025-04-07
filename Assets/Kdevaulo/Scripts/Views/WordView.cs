using Kdevaulo.WordPuzzle.Data;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Kdevaulo.WordPuzzle.Views
{
    [AddComponentMenu(nameof(WordView) + " in " + nameof(Views))]
    public class WordView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CellView[] _cells;

        private CellColors _cellColors;

        private bool[] _cellsOccupancy;

        private bool _isPointerOver;

        private void Awake()
        {
            var colors = new CellColors()
            {
                HighlightColor = Color.green,
                OccupiedColor = Color.yellow,
                NormalColor = Color.white
            };

            Initialize(colors);
        }

        private void Update()
        {
            if (_isPointerOver && ClusterView.CurrentDragged != null)
            {
                TryHighlightCells(ClusterView.CurrentDragged.ClusterLength);
            }
            else
            {
                UpdateCellsColors();
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("PointerEnter");
            _isPointerOver = true;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("PointerExit");
            _isPointerOver = false;
            UpdateCellsColors();
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            var cluster = ClusterView.CurrentDragged;
            if (cluster == null) return;

            var index = FindFreeSegment(cluster.ClusterLength);

            if (index >= 0)
            {
                // todo: Выставляем позицию кластера так, чтобы буквы встали в нужные ячейки
            }

            UpdateCellsColors();
        }

        public void Initialize(CellColors colors)
        {
            _cellColors = colors;
            _cellsOccupancy = new bool[_cells.Length];
        }

        private void TryHighlightCells(int neededCount)
        {
            var index = FindFreeSegment(neededCount);

            if (index >= 0)
            {
                for (int i = index; i < index + neededCount; i++)
                {
                    _cells[i].SetColor(_cellColors.HighlightColor);
                }
            }
            else
            {
                UpdateCellsColors();
            }
        }

        private int FindFreeSegment(int neededCount)
        {
            var consecutive = 0;
            var startIndex = 0;

            var cellsCount = _cellsOccupancy.Length;

            for (int i = 0; i < cellsCount; i++)
            {
                if (!_cellsOccupancy[i])
                {
                    consecutive++;
                }
                else
                {
                    consecutive = 0;
                    startIndex = i + 1;
                }

                if (consecutive == neededCount)
                {
                    return startIndex;
                }
            }

            return -1;
        }

        private void UpdateCellsColors()
        {
            var cellsCount = _cellsOccupancy.Length;

            for (int i = 0; i < cellsCount; i++)
            {
                _cells[i].SetColor(_cellsOccupancy[i]
                    ? _cellColors.OccupiedColor
                    : _cellColors.NormalColor);
            }
        }
    }
}