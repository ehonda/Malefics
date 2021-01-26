using System;
using System.Collections.Generic;
using System.Linq;
using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Models.Pieces;

namespace MaleficsTests.Models.Pieces.TestCases
{
    public static class PieceCases
    {
        public static IEnumerable<Piece> AllPieces
            => Barricade.Concat<Piece>(Pawns);

        public static IEnumerable<(Piece, Piece)> AllPairs
            => AllPieces.SelectMany(p => AllPieces.Select(q => (p, q)));

        public static IEnumerable<(Piece, Piece)> UnequalPairs
            => AllPairs.Where(p => p.First() != p.Second());

        public static IEnumerable<Barricade> Barricade
            => Enumerable.Repeat(new Barricade(), 1);

        public static IEnumerable<Pawn> Pawns
            => Enum
                .GetValues<PlayerColor>()
                .Select(player => new Pawn(player));

        public static IEnumerable<(Pawn, Pawn)> PawnPairs
            => Pawns.SelectMany(p => Pawns.Select(q => (p, q)));

        public static IEnumerable<(Pawn, Pawn)> PawnPairsOfDifferentColors
            => PawnPairs.Where(pawns => pawns.First() != pawns.Second());

    }
}