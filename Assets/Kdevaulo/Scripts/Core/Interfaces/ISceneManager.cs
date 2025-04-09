using System.Threading;

using Cysharp.Threading.Tasks;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface ISceneManager
    {
        UniTask LoadSceneAsync(int sceneIndex, CancellationToken token);
        UniTask UnloadSceneAsync(int sceneIndex, CancellationToken token);
        void SetSceneActive(int sceneIndex);
        int GetCurrentSceneIndex();
    }
}