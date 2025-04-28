using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Model;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class LevelLoadingSystem
    {
        private Level _loadedLevel;
        private LocalLevelLoader _levelLoader;

        public int GetLevelsCount()
        {
            PrepareLevelLoader();
            return _levelLoader.GetLevelsCount();
        }

        public Level GetLoadedLevel()
        {
            return _loadedLevel;
        }

        public async UniTask LoadLevelAsync(int level, CancellationToken token)
        {
            PrepareLevelLoader();

            _loadedLevel = await _levelLoader.TryLoadLevelAsync(level, token);
        }

        private void PrepareLevelLoader()
        {
            if (_levelLoader == null)
            {
                _levelLoader = new LocalLevelLoader();
            }
        }
    }
}