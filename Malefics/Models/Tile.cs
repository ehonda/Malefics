using Malefics.Enums;
using Malefics.Models.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Models
{
    public class Tile
    {
        public Terrain Terrain { get; init; }

        public IEnumerable<IPiece> OccupyingPieces { get; set; }
            = Enumerable.Empty<IPiece>();

        public bool IsOccupied() => OccupyingPieces is not null;

        public static Tile Rock() => new() { Terrain = Terrain.Rock };

        public static Tile Road() => new() { Terrain = Terrain.Road };

        public static Tile Barricade() => new()
        {
            Terrain = Terrain.Road,
            OccupyingPieces = new[] { new Barricade() }
        };

        public static Tile Pawn(Player player) => new()
        {
            Terrain = Terrain.Road,
            OccupyingPieces = new[] { new Pawn { Player = player } }
        };
    }
}
