using System;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IValidationService
    {
        public event Action ValidationFailed;
        public event Action ValidationSucceed;

        public void ValidateWords();
        public void ValidateWord(string solvedWord);
    }
}