using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Model;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class LocalLevelLoader
    {
        private const int LevelsCount = 4;

        public int GetLevelsCount()
        {
            return LevelsCount;
        }

        public async UniTask<Level> TryLoadLevelAsync(int levelId, CancellationToken _)
        {
            var path = $"BuiltInLevels/Level{levelId}";
            var textAsset = Resources.Load<TextAsset>(path);

            if (textAsset == null)
            {
                Debug.LogError($"Не найден файл JSON по пути: {path}");
                return null;
            }

            await UniTask.CompletedTask;

            return JsonUtility.FromJson<Level>(textAsset.text);
        }
    }
}