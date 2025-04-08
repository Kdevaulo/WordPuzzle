using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using UnityEngine;

namespace Kdevaulo.WordPuzzle.Installers
{
    public class LocalLevelLoader : ILevelLoader
    {
        async UniTask<Level> ILevelLoader.TryLoadLevelAsync(int levelId, CancellationToken token)
        {
            var path = $"BuiltInLevels/Level{levelId}";
            var textAsset = Resources.Load<TextAsset>(path);

            if (textAsset == null)
            {
                Debug.LogError($"Не найден файл JSON по пути: {path}");
                return null;
            }

            return JsonUtility.FromJson<Level>(textAsset.text);
        }
    }
}