using Malefics.Models.Pieces;

namespace Malefics.Models
{
    public class Node
    {
        public IPiece? OccupyingPiece { get; set; }

        public bool IsOccupied() => OccupyingPiece is not null;

        public static Node OccupiedBy(IPiece piece)
            => new Node { OccupyingPiece = piece };

        public static Node Unoccupied() => new();
    }
}
