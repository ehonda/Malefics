using Malefics.Models.Pieces;

namespace Malefics.Models
{
    public class Tile
    {
        public Terrain Terrain { get; init; }

        public IPiece? OccupyingPiece { get; set; }

        public bool IsOccupied() => OccupyingPiece is not null;

        public static Tile OccupiedBy(IPiece piece)
            => new Tile { OccupyingPiece = piece };

        public static Tile Unoccupied() => new();
    }
}
