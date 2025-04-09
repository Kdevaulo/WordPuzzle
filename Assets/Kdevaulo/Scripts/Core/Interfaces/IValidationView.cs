namespace Kdevaulo.WordPuzzle.Core
{
    public interface IValidationView
    {
        public void HandleSuccess();
        public void HandleFail();
    }
}