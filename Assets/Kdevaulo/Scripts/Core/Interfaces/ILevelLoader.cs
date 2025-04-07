using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface ILevelLoader
    {
        public UniTask<Level> TryLoadLevelAsync(int levelId, CancellationToken token);
    }
}