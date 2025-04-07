using System.Threading;

using Cysharp.Threading.Tasks;

using Zenject;

namespace Kdevaulo.WordPuzzle.Core
{
    public class GameController : IInitializable
    {
        private readonly AbstractLevelLoader _levelLoader;
        private readonly IClusterSpawner _clusterSpawner;

        private CancellationTokenSource _cts;

        public GameController(AbstractLevelLoader levelLoader, IClusterSpawner clusterSpawner)
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