using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using Zenject;

namespace Kdevaulo.WordPuzzle.Service
{
    public class SceneService : ISceneService
    {
        public event Action SwitchFinished;
        public event Action SwitchStarted;

        [Inject]
        private ISceneManager _sceneManager;

        async UniTask ISceneService.SwitchSceneAsync(SceneType sceneType, bool unloadPrevious, CancellationToken token)
        {
            var sceneIndex = (int) sceneType;

            SwitchStarted?.Invoke();

            var sceneToUnloadIndex = _sceneManager.GetCurrentSceneIndex();

            await _sceneManager.LoadSceneAsync(sceneIndex, token);

            _sceneManager.SetSceneActive(sceneIndex);

            if (unloadPrevious)
            {
                await _sceneManager.UnloadSceneAsync(sceneToUnloadIndex, token);
            }

            SwitchFinished?.Invoke();
        }
    }
}