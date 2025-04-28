using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Model;
using Kdevaulo.WordPuzzle.Service;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class StartupPresenter : IInitializable
    {
        [Inject]
        private SceneChangeSystem _sceneSystem;

        private CancellationTokenSource _cts;

        void IInitializable.Initialize()
        {
            _cts = new CancellationTokenSource();
            _sceneSystem.SwitchSceneAsync(SceneType.MainMenu, false, _cts.Token).Forget();
        }
    }
}