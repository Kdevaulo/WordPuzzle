using System.Collections.Generic;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Model
{
    public class SessionModel : ISessionModel
    {
        Level ISessionModel.CurrentLevel { get; set; }
        string[] ISessionModel.SolvedWords => _solvedWords.ToArray();

        private List<string> _solvedWords = new List<string>();

        void ISessionModel.SetSolvedWord(string solvedWord)
        {
            if (_solvedWords.Contains(solvedWord))
            {
                _solvedWords.Remove(solvedWord);
            }

            _solvedWords.Add(solvedWord);
        }

        void ISessionModel.ClearSolvedWords()
        {
            _solvedWords.Clear();
        }
    }
}