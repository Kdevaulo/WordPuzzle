using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class MainMenuPresenter : IMainMenuPresenter
    {
        [Inject]
        private ISceneService _sceneService;
        [Inject]
        private IMainMenuView _view;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        private bool _isSwitchingScene;

        void IMainMenuPresenter.HandlePlayClick()
        {
            _view.DisableButtons();
            SwitchScene(SceneType.Game);
        }

        void IMainMenuPresenter.HandleSettingsClick()
        {
            _view.DisableButtons();
            SwitchScene(SceneType.Settings);
        }

        private void SwitchScene(SceneType scene)
        {
            _sceneService.SwitchSceneAsync(scene, true, _cts.Token).Forget();
        }
    }
}