using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class StartupPresenter : IInitializable
    {
        [Inject]
        private ISceneService _sceneService;

        private CancellationTokenSource _cts;

        void IInitializable.Initialize()
        {
            _cts = new CancellationTokenSource();
            _sceneService.SwitchSceneAsync(SceneType.MainMenu, false, _cts.Token).Forget();
        }
    }
}