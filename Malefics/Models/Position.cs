using System;

namespace Malefics.Models
{
    public record Position(int X, int Y)
    {
        public uint ManhattanDistanceTo(Position p)
            => (uint)Math.Abs(X - p.X) + (uint)Math.Abs(Y - p.Y);

        public bool IsNeighborOf(Position p)
            => ManhattanDistanceTo(p) == 1u;
    }
}
