using System;
using System.Collections.Generic;
using System.Linq;
using Malefics.Enums;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    // TODO: Convert to subclasses Road, Rock, etc. of ITile - Tests for subclasses!
    public class Tile : ITile
    {
        public Terrain Terrain { get; init; }

        private IList<IPiece> _occupyingPieces = new List<IPiece>();

        public void Add(IPiece piece)
        {
            if (Terrain is Terrain.Rock)
                throw new InvalidOperationException("Can't add piece to a rock tile.");

            _occupyingPieces.Add(piece);
        }

        public void Remove(IPiece piece) => _occupyingPieces.Remove(piece);

        public bool IsBarricaded() => _occupyingPieces.Contains(new Barricade());

        public bool IsOccupied() => _occupyingPieces.Any();

        public static ITile Rock() => new Tile() { Terrain = Terrain.Rock };

        public static ITile Road() => new Tile() { Terrain = Terrain.Road };

        public static ITile Barricade() => new Tile()
        {
            Terrain = Terrain.Road,
            _occupyingPieces = new[] { new Barricade() }
        };

        public static ITile Pawn(Player player) => new Tile()
        {
            Terrain = Terrain.Road,
            _occupyingPieces = new[] { new Pawn(player) }
        };

        public static Tile House(Player player, int numberOfPawns) => new()
        {
            Terrain = Terrain.House,
            _occupyingPieces = Enumerable
                .Repeat(0, numberOfPawns)
                .Select(_ => new Pawn(player))
                .AsEnumerable<IPiece>()
                .ToList()
        };
    }
}
