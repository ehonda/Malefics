using Malefics.Models.Pieces;

namespace Malefics.Models
{
    public class Tile
    {
        public Terrain Terrain { get; init; }

        public IPiece? OccupyingPiece { get; set; }

        public bool IsOccupied() => OccupyingPiece is not null;

        // TODO: Do we need this? It should have a better name!
        public static Tile OccupiedBy(IPiece piece)
            => new Tile { OccupyingPiece = piece };

        // TODO: Do we need this? It should have a better name!
        public static Tile Unoccupied() => new();
    }
}
