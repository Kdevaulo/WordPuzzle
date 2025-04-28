using System.Collections.Generic;

namespace Kdevaulo.WordPuzzle.Model
{
    public class SessionModel
    {
        public Level CurrentLevel { get; set; }
        public string[] SolvedWords => _solvedWords.ToArray();

        private List<string> _solvedWords = new List<string>();

        public void SetSolvedWord(string solvedWord)
        {
            if (_solvedWords.Contains(solvedWord))
            {
                _solvedWords.Remove(solvedWord);
            }

            _solvedWords.Add(solvedWord);
        }

        public void ClearSolvedWords()
        {
            _solvedWords.Clear();
        }
    }
}