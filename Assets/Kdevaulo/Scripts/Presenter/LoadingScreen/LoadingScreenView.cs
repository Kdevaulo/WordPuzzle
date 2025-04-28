using UnityEngine;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;

        public void EnableLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        public void DisableLoadingScreen()
        {
            _loadingScreen.SetActive(false);
        }
    }
}