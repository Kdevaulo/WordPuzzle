using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core.Data;

using Zenject;

namespace Kdevaulo.WordPuzzle.Core
{
    public class AbstractLevelLoader
    {
        [Inject]
        private ILevelLoader _levelLoader;
        private Level _loadedLevel;

        public Level GetLoadedLevel()
        {
            return _loadedLevel;
        }

        public async UniTask TryLoadLevelAsync(int levelId, CancellationToken token)
        {
            _loadedLevel = await _levelLoader.TryLoadLevelAsync(levelId, token);
        }
    }
}