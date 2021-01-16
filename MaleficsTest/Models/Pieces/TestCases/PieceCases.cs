using Malefics.Enums;
using Malefics.Models.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace MaleficsTests.Models.Pieces.TestCases
{
    public static class PieceCases
    {
        public static IEnumerable<IPiece> All
        {
            get
            {
                yield return new Barricade();
                yield return new Pawn(Player.Red);
                yield return new Pawn(Player.Green);
                yield return new Pawn(Player.Yellow);
                yield return new Pawn(Player.Blue);
            }
        }

        public static IEnumerable<Pawn> Pawns
        {
            get
            {
                yield return new Pawn(Player.Red);
                yield return new Pawn(Player.Green);
                yield return new Pawn(Player.Yellow);
                yield return new Pawn(Player.Blue);
            }
        }

        public static IEnumerable<(Pawn, Pawn)> PawnsOfDifferentColors =>
            Pawns
                .SelectMany(p => Pawns
                    .Select(q => (p, q))
                    .Where(pawns => pawns.p != pawns.q));
    }
}