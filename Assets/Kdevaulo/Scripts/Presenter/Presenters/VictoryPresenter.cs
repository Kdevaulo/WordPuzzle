using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Model;
using Kdevaulo.WordPuzzle.Service;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class VictoryPresenter : IInitializable
    {
        [Inject]
        private SceneChangeSystem _sceneSystem;
        [Inject]
        private VictoryView _view;
        [Inject]
        private SessionModel _sessionModel;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        void IInitializable.Initialize()
        {
            var victoryData = _sessionModel.SolvedWords;
            _view.SetText(victoryData);
            _sessionModel.ClearSolvedWords();
        }

        public void HandleMenuClick()
        {
            _sceneSystem.SwitchSceneAsync(SceneType.MainMenu, true, _cts.Token).Forget();
        }

        public void HandleNextClick()
        {
            _sceneSystem.SwitchSceneAsync(SceneType.Game, true, _cts.Token).Forget();
        }
    }
}