using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Model;

using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Kdevaulo.WordPuzzle.Service
{
    public class SceneChangeSystem
    {
        public event Action SwitchFinished;
        public event Action SwitchStarted;

        public async UniTask SwitchSceneAsync(SceneType sceneType, bool unloadPrevious, CancellationToken token)
        {
            var sceneIndex = (int) sceneType;

            SwitchStarted?.Invoke();

            var sceneToUnloadIndex = SceneManager.GetActiveScene().buildIndex;

            await SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive).ToUniTask(cancellationToken: token);

            Assert.IsTrue(sceneIndex > 0);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));

            if (unloadPrevious)
            {
                await SceneManager.UnloadSceneAsync(sceneToUnloadIndex).ToUniTask(cancellationToken: token);
            }

            SwitchFinished?.Invoke();
        }
    }
}