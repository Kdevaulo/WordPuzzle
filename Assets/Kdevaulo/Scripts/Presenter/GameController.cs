using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core;

using ModestTree;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class GameController : IInitializable
    {
        [Inject]
        private LevelLoaderService _levelLoader;
        [Inject]
        private IClusterSpawner _clusterSpawner;
        [Inject]
        private ISessionModel _model;

        private CancellationTokenSource _cts;

        void IInitializable.Initialize()
        {
            _cts = new CancellationTokenSource();
            TryStartLevelAsync(_cts.Token).Forget();
        }

        private async UniTask TryStartLevelAsync(CancellationToken token)
        {
            await _levelLoader.TryLoadLevelAsync(1, token);

            var loadedLevel = _levelLoader.GetLoadedLevel();
            Assert.IsNotNull(loadedLevel);

            _model.SetLevel(loadedLevel);

            if (loadedLevel?.Words != null && loadedLevel.Words.Length != 0)
            {
                _clusterSpawner.CreateClusters(loadedLevel);
            }
        }
    }
}