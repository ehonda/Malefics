using Malefics.Models.Pieces;

namespace Malefics.Models
{
    public class Tile
    {
        public Terrain Terrain { get; init; }

        public IPiece? OccupyingPiece { get; set; }

        public bool IsOccupied() => OccupyingPiece is not null;

        public static Tile Rock() => new() { Terrain = Terrain.Rock };

        public static Tile Road() => new() { Terrain = Terrain.Road };

        public static Tile Barricade() => new()
        {
            Terrain = Terrain.Road,
            OccupyingPiece = new Barricade()
        };
    }
}
