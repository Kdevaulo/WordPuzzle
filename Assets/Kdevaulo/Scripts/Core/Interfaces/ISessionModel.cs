using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface ISessionModel
    {
        public Level CurrentLevel { get; set; }
        public string[] SolvedWords { get; }
        void SetSolvedWord(string solvedWords);
        public void ClearSolvedWords();
    }
}