using System;

using Kdevaulo.WordPuzzle.Service;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class LoadingScreenPresenter : IInitializable, IDisposable
    {
        [Inject]
        private SceneChangeSystem _sceneSystem;
        [Inject]
        private LoadingScreenView _view;

        void IInitializable.Initialize()
        {
            _sceneSystem.SwitchFinished += HideLoadingScreen;
            _sceneSystem.SwitchStarted += ShowLoadingScreen;
        }

        void IDisposable.Dispose()
        {
            _sceneSystem.SwitchFinished -= HideLoadingScreen;
            _sceneSystem.SwitchStarted -= ShowLoadingScreen;
        }

        private void ShowLoadingScreen()
        {
            _view.EnableLoadingScreen();
        }

        private void HideLoadingScreen()
        {
            _view.DisableLoadingScreen();
        }
    }
}