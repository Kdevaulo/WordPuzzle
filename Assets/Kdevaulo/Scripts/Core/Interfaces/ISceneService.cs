using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface ISceneService
    {
        public event Action SwitchStarted;
        public event Action SwitchFinished;
        public UniTask SwitchSceneAsync(SceneType sceneType, bool unloadPrevious, CancellationToken token);
    }
}