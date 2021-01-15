using Malefics.Enums;
using Malefics.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Models
{
    public class Tile
    {
        public Terrain Terrain { get; init; }

        private IList<IPiece> _occupyingPieces = new List<IPiece>();

        public void Add(IPiece piece)
        {
            if (Terrain is Terrain.Rock)
                throw new InvalidOperationException("Can't add piece to a rock tile.");

            _occupyingPieces.Add(piece);
        }

        public void RemoveFirst() => _occupyingPieces.RemoveAt(0);

        public bool IsBarricaded() => _occupyingPieces
            .Contains(new Barricade());

        public bool IsOccupied() => _occupyingPieces.Any();

        public static Tile Rock() => new() { Terrain = Terrain.Rock };

        public static Tile Road() => new() { Terrain = Terrain.Road };

        public static Tile Barricade() => new()
        {
            Terrain = Terrain.Road,
            _occupyingPieces = new[] { new Barricade() }
        };

        public static Tile Pawn(Player player) => new()
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
