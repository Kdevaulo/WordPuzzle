using UnityEngine;

namespace Kdevaulo.WordPuzzle.Presenter
{
    [DisallowMultipleComponent]
    [AddComponentMenu(nameof(FrameRateLimiter) + " in " + nameof(Presenter))]
    public class FrameRateLimiter : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}