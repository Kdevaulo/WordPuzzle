namespace Kdevaulo.WordPuzzle.Model
{
    public class Cluster
    {
        public string Name { get; private set; }
        public int Length { get; private set; }

        public Cluster(string name)
        {
            Name = name;
            Length = name.Length;
        }
    }
}