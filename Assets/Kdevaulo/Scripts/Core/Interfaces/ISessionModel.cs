using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface ISessionModel
    {
        public void SetLevel(Level level);
        public Level TryGetLevel();
    }
}