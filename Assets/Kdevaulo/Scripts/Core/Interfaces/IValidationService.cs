using System;

using Kdevaulo.WordPuzzle.Core.Data;

namespace Kdevaulo.WordPuzzle.Core
{
    public interface IValidationService
    {
        event Action ValidationFailed;
        event Action ValidationSucceed;

        public void ValidateWords(Level level);
    }
}