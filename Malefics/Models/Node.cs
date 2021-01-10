namespace Malefics.Models
{
    public class Node
    {
        public IPiece? OccupyingPiece { get; set; }

        public bool IsOccupied() => OccupyingPiece is not null;
    }
}
