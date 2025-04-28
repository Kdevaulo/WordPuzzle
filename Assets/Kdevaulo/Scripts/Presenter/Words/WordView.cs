using System;
using System.Linq;

using Kdevaulo.WordPuzzle.Model;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class WordView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnDrop;
        public event Action<bool> OnPointerOverChange;

        [Header("Values")]
        [SerializeField] private Vector2 _anchorMin = Vector2.one / 2f;
        [SerializeField] private Vector2 _anchorMax = Vector2.one / 2f;
        [SerializeField] private Vector2 _pivot = Vector2.one / 2f;

        [Header("References")]
        [SerializeField] private RectTransform _transform;
        [SerializeField] private CellView[] _cells;

        private CellColors _cellColors;

        [Inject]
        public void Construct(WordsConfig wordsConfig)
        {
            _cellColors = wordsConfig.Colors;
        }

        public Vector2[] GetPositions()
        {
            return _cells.Select(x => x.GetPosition()).ToArray();
        }

        public Anchor GetAnchorPreset()
        {
            return new Anchor()
            {
                AnchorMax = _anchorMax,
                AnchorMin = _anchorMin,
                Pivot = _pivot
            };
        }

        public Transform GetTransform()
        {
            return _transform;
        }

        public void SetCellState(int cellIndex, State state)
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
            OnPointerOverChange?.Invoke(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            OnPointerOverChange?.Invoke(false);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            OnDrop?.Invoke();
        }
    }
}