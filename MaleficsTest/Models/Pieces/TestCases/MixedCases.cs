using System.Collections.Generic;
using System.Linq;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
// ReSharper disable InconsistentNaming

namespace MaleficsTests.Models.Pieces.TestCases
{
    public static class MixedCases
    {
        public static IEnumerable<(House, Piece)> AllEmptyHouses_AllPieces
            => HouseCases.AllEmptyHouses
                .SelectMany(house => 
                    PieceCases.AllPieces.Select(piece => (house, piece)));

        public static IEnumerable<(House, Pawn)> AllEmptyHouses_PawnsOfDifferentColor
            => HouseCases.AllEmptyHouses
                .SelectMany(house =>
                    PieceCases.Pawns
                        .Select(pawn => (house, pawn))
                        .Where(houseAndPawn => 
                            houseAndPawn.house.Player != houseAndPawn.pawn.Player));
    }
}