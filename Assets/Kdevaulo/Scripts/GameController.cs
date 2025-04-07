using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Data;

using UnityEngine.Assertions;

using Zenject;

namespace Kdevaulo.WordPuzzle
{
    public class GameController : IInitializable
    {
        private readonly LevelLoader _levelLoader;
        private readonly ClusterSpawner _clusterSpawner;

        private CancellationTokenSource _cts;

        public GameController(LevelLoader levelLoader, ClusterSpawner clusterSpawner)
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

            if (loadedLevel != null)
            {
                Assert.IsFalse(loadedLevel.Words == null);
                Assert.IsFalse(loadedLevel.Words.Length == 0);

                _clusterSpawner.CreateClusters(loadedLevel);
            }
        }
    }
}