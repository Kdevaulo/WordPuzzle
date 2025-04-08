namespace Kdevaulo.WordPuzzle.Core
{
    public interface ICanvasParamsProvider
    {
        public float GetScaleFactor();
        public ITransform GetTransform();
    }
}