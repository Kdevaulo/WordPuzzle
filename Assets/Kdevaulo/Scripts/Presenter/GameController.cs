using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class GameController : IInitializable
    {
        private readonly LevelLoaderService _levelLoader;
        private readonly IClusterSpawner _clusterSpawner;

        private CancellationTokenSource _cts;

        public GameController(LevelLoaderService levelLoader, IClusterSpawner clusterSpawner)
        {
            _levelLoader = levelLoader;
            _clusterSpawner = clusterSpawner;
        }

        void IInitializable.Initialize()
        {
            _cts = new CancellationTokenSource();
            TryStartLevelAsync(_cts.Token).Forget();
        }

        private async UniTask TryStartLevelAsync(CancellationToken token)
        {
            await _levelLoader.TryLoadLevelAsync(1, token);

            var loadedLevel = _levelLoader.GetLoadedLevel();

            if (loadedLevel?.Words != null && loadedLevel.Words.Length != 0)
            {
                _clusterSpawner.CreateClusters(loadedLevel);
            }
        }
    }
}