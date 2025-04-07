using UnityEngine;
using UnityEngine.EventSystems;

namespace Kdevaulo.WordPuzzle.presentation.Utilities
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    [AddComponentMenu(nameof(SafeAreaFitter) + " in " + nameof(Utilities))]
    public class SafeAreaFitter : UIBehaviour
    {
        private Rect _lastSafeArea = Rect.zero;

        private RectTransform _rectTransform;

        protected override void OnRectTransformDimensionsChange()
        {
            TryApplySafeArea();
        }

        private void TryApplySafeArea()
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

            var safeArea = Screen.safeArea;

            if (safeArea != _lastSafeArea)
            {
                _lastSafeArea = safeArea;
                ApplySafeArea(safeArea);
            }
        }

        private void ApplySafeArea(Rect safeArea)
        {
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;

            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
        }
    }
}