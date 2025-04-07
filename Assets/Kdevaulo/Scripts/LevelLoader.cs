using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Data;

using UnityEngine;

namespace Kdevaulo.WordPuzzle
{
    public class LevelLoader
    {
        private Level _loadedLevel;

        public Level GetLoadedLevel()
        {
            return _loadedLevel;
        }

        public async UniTask TryLoadLevelAsync(int levelId, CancellationToken token)
        {
            var path = $"BuiltInLevels/Level{levelId}";
            var textAsset = Resources.Load<TextAsset>(path);

            if (textAsset == null)
            {
                _loadedLevel = null;
                Debug.LogError($"Не найден файл JSON по пути: {path}");
            }
            else
            {
                _loadedLevel = JsonUtility.FromJson<Level>(textAsset.text);
            }

            await UniTask.CompletedTask;
        }
    }
}