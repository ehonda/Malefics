using Malefics.Enums;
using Malefics.Models.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Models
{
    public class Tile
    {
        public Terrain Terrain { get; init; }

        private IList<IPiece> _occupyingPieces = new List<IPiece>();

        public IList<IPiece> OccupyingPieces 
        { 
            get => _occupyingPieces;
            init => _occupyingPieces = value.ToList();
        }

        public bool IsOccupied() => OccupyingPieces.Any();

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
