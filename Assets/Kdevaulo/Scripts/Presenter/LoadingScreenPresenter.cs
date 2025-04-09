using System;

using Kdevaulo.WordPuzzle.Core;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class LoadingScreenPresenter : IInitializable, IDisposable
    {
        [Inject]
        private ISceneService _sceneService;
        [Inject]
        private ISceneView _view;

        void IInitializable.Initialize()
        {
            _sceneService.SwitchFinished += HideLoadingScreen;
            _sceneService.SwitchStarted += ShowLoadingScreen;
        }

        void IDisposable.Dispose()
        {
            _sceneService.SwitchFinished -= HideLoadingScreen;
            _sceneService.SwitchStarted -= ShowLoadingScreen;
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