using System.Threading;

using Cysharp.Threading.Tasks;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface ISceneManager
    {
        public UniTask LoadSceneAsync(int sceneIndex, CancellationToken token);
        public UniTask UnloadSceneAsync(int sceneIndex, CancellationToken token);
        public void SetSceneActive(int sceneIndex);
        public int GetCurrentSceneIndex();
    }
}