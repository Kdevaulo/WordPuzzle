using System;
using System.Linq;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;
using Kdevaulo.WordPuzzle.View.Data;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

using Zenject;

namespace Kdevaulo.WordPuzzle.View
{
    [AddComponentMenu(nameof(WordView) + " in " + nameof(View))]
    public class WordView : MonoBehaviour, IWordView, IDropHandler, IPointerEnterHandler, IPointerExitHandler, ITickable
    {
        [Header("Values")]
        [SerializeField] private Vector2 _anchorMin = Vector2.one / 2f;
        [SerializeField] private Vector2 _anchorMax = Vector2.one / 2f;
        [SerializeField] private Vector2 _pivot = Vector2.one / 2f;

        [Header("References")]
        [SerializeField] private RectTransform _transform;
        [SerializeField] private CellView[] _cells;

        private IWordPresenter _wordPresenter;

        private DragHandler _dragHandler;
        private CellColors _cellColors;

        private bool _isPointerOver;

        [Inject]
        public void Construct(IWordPresenter wordPresenter, DragHandler dragHandler, WordsData wordsData)
        {
            _wordPresenter = wordPresenter;
            _dragHandler = dragHandler;
            _cellColors = wordsData.Colors;

            var cellPositions = _cells.Select(x => x.GetPosition().ToNumerics()).ToArray();
            _wordPresenter.SetSells(this, cellPositions);
        }

        void ITickable.Tick()
        {
            _wordPresenter.ClearSelected(this);

            if (_isPointerOver && _dragHandler.CurrentDraggingView != null)
            {
                var position = (Vector2) _dragHandler.CurrentDraggingView.transform.position;
                var cellsCount = _dragHandler.CurrentDraggingView.ClusterLength;

                _wordPresenter.TryHighlightCells(this, position.ToNumerics(), cellsCount);
            }
        }

        void IWordView.SetCellState(int cellIndex, State state)
        {
            Assert.IsTrue(cellIndex >= 0 && cellIndex < _cells.Length);
            Assert.IsFalse(state == State.None);

            var targetColor = state switch
            {
                State.Selected => _cellColors.HighlightColor,
                State.Occupied => _cellColors.OccupiedColor,
                State.Free => _cellColors.NormalColor,

                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };

            _cells[cellIndex].SetColor(targetColor);
        }

        void IWordView.HandleCellsOccupied(System.Numerics.Vector2 position)
        {
            var cluster = _dragHandler.CurrentDraggingView;
            cluster.SetParent(_transform);
            cluster.SetAnchorPreset(_anchorMin, _anchorMax, _pivot);
            cluster.SetPosition(position.ToUnity());
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
            // todo: move to cluster logic

            var cluster = _dragHandler.CurrentDraggingView;
            if (cluster == null)
                return;

            _wordPresenter.TryOccupyCells(this);
        }
    }
}