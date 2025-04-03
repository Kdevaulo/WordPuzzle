using UnityEngine;

namespace Kdevaulo.WordPuzzle.Utilities
{
    [DisallowMultipleComponent]
    [AddComponentMenu(nameof(FrameRateLimiter) + " in " + nameof(Utilities))]
    public class FrameRateLimiter : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}