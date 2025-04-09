using System;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IValidationService
    {
        public event Action ValidationFailed;
        public event Action ValidationSucceed;

        public void ValidateWords(Level level);
    }
}