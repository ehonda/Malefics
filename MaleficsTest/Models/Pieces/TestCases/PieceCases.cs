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
        public static IEnumerable<IPiece> AllPieces
            => Barricade.Concat<IPiece>(Pawns);

        public static IEnumerable<(IPiece, IPiece)> AllPairs
            => AllPieces.SelectMany(p => AllPieces.Select(q => (p, q)));

        public static IEnumerable<Barricade> Barricade
            => Enumerable.Repeat(new Barricade(), 1);

        public static IEnumerable<Pawn> Pawns
            => Enum
                .GetValues<Player>()
                .Select(player => new Pawn(player));

        public static IEnumerable<(Pawn, Pawn)> PawnPairs
            => Pawns.SelectMany(p => Pawns.Select(q => (p, q)));

        public static IEnumerable<(Pawn, Pawn)> PawnPairsOfDifferentColors
            => PawnPairs.Where(pawns => pawns.First() != pawns.Second());
    }
}