using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core.Data;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class LevelLoadingSystem
    {
        private const string CurrentLevelKey = "CurrentLevelKey";

        private Level _loadedLevel;
        private LocalLevelLoader _levelLoader;

        private int _levelsCount;
        private int _currentLevel;

        public Level GetLoadedLevel()
        {
            return _loadedLevel;
        }

        public async UniTask LoadNextLevelAsync(CancellationToken token)
        {
            if (++_currentLevel > _levelsCount)
            {
                _currentLevel = 1;
            }

            if (_levelLoader == null)
            {
                _levelLoader = new LocalLevelLoader();
                _levelsCount = _levelLoader.GetLevelsCount();
                _currentLevel = PlayerPrefs.GetInt(CurrentLevelKey, 0);
            }

            PlayerPrefs.SetInt(CurrentLevelKey, _currentLevel);
            PlayerPrefs.Save();
            _loadedLevel = await _levelLoader.TryLoadLevelAsync(_currentLevel, token);
        }
    }
}