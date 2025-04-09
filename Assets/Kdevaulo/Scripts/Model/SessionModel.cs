using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Model
{
    public class SessionModel : ISessionModel
    {
        private Level _currentLevel;

        public void SetLevel(Level level)
        {
            _currentLevel = level;
        }

        Level ISessionModel.TryGetLevel()
        {
            return _currentLevel;
        }
    }
}