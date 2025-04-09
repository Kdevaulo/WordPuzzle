using Kdevaulo.WordPuzzle.Core;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.View
{
    [AddComponentMenu(nameof(LoadingScreenView) + " in " + nameof(View))]
    public class LoadingScreenView : MonoBehaviour, ISceneView
    {
        [SerializeField] private GameObject _loadingScreen;

        void ISceneView.EnableLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        void ISceneView.DisableLoadingScreen()
        {
            _loadingScreen.SetActive(false);
        }
    }
}