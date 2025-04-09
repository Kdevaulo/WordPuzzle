using System.Threading;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class VictoryPresenter : IVictoryPresenter, IInitializable
    {
        [Inject]
        private ISceneService _sceneService;
        [Inject]
        private IVictoryView _view;
        [Inject]
        private ISessionModel _sessionModel;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        void IVictoryPresenter.HandleMenuClick()
        {
            _sceneService.SwitchSceneAsync(SceneType.MainMenu, true, _cts.Token);
        }

        void IVictoryPresenter.HandleNextClick()
        {
            // todo: call current level change
            _sceneService.SwitchSceneAsync(SceneType.Game, true, _cts.Token);
        }

        void IInitializable.Initialize()
        {
            var victoryData = _sessionModel.SolvedWords;
            _view.SetText(victoryData);
            _sessionModel.ClearSolvedWords();
        }
    }
}