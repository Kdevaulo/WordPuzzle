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
    public class WordView : MonoBehaviour,
        IWordView, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Values")]
        [SerializeField] private Vector2 _anchorMin = Vector2.one / 2f;
        [SerializeField] private Vector2 _anchorMax = Vector2.one / 2f;
        [SerializeField] private Vector2 _pivot = Vector2.one / 2f;

        [Header("References")]
        [SerializeField] private RectTransform _transform;
        [SerializeField] private CellView[] _cells;

        private IWordPresenter _wordPresenter;

        private CellColors _cellColors;

        private bool _isPointerOver;

        private TransformAdapter _transformAdapter;

        [Inject]
        public void Construct(IWordPresenter wordPresenter, WordsData wordsData)
        {
            _wordPresenter = wordPresenter;
            _cellColors = wordsData.Colors;

            var cellPositions = _cells.Select(x => x.GetPosition().ToNumerics()).ToArray();
            _wordPresenter.InitializeWord(this, cellPositions);
        }

        AnchorPreset IWordView.GetAnchorPreset()
        {
            return new AnchorPreset()
            {
                AnchorMax = _anchorMax.ToNumerics(),
                AnchorMin = _anchorMin.ToNumerics(),
                Pivot = _pivot.ToNumerics()
            };
        }

        ITransform IWordView.GetTransform()
        {
            return _transformAdapter ?? new TransformAdapter(_transform);
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

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            _wordPresenter.SetIsPointerOver(this, true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _wordPresenter.SetIsPointerOver(this, false);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            _wordPresenter.TryOccupyCells(this);
        }
    }
}