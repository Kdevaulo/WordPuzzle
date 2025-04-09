using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core;

using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Kdevaulo.WordPuzzle.View
{
    public class SceneManagerAdapter : ISceneManager
    {
        async UniTask ISceneManager.LoadSceneAsync(int sceneIndex, CancellationToken token)
        {
            await SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive).ToUniTask(cancellationToken: token);
        }

        async UniTask ISceneManager.UnloadSceneAsync(int sceneIndex, CancellationToken token)
        {
            await SceneManager.UnloadSceneAsync(sceneIndex).ToUniTask(cancellationToken: token);
        }

        void ISceneManager.SetSceneActive(int sceneIndex)
        {
            Assert.IsTrue(sceneIndex > 0);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
        }

        int ISceneManager.GetCurrentSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
}