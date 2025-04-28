using System;
using System.Numerics;

namespace Kdevaulo.WordPuzzle.Model
{
    public class CellModel
    {
        public event Action StateChanged;

        public int Id { get; private set; }
        public Vector2 Position { get; private set; }
        public Cluster Cluster { get; private set; }
        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                StateChanged?.Invoke();
            }
        }

        private State _currentState;

        public CellModel(int id, Vector2 position)
        {
            Id = id;
            Position = position;
            CurrentState = State.Free;
        }

        public void Occupy(Cluster cluster)
        {
            Cluster = cluster;
            CurrentState = State.Occupied;
        }

        public void Free()
        {
            Cluster = null;
            CurrentState = State.Free;
        }
    }
}