using System.Collections.Generic;
using System.Linq;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
// ReSharper disable InconsistentNaming

namespace MaleficsTests.Models.Pieces.TestCases
{
    public static class MixedCases
    {
        public static IEnumerable<(House, IPiece)> AllEmptyHouses_AllPieces
            => HouseCases.AllEmptyHouses
                .SelectMany(house => 
                    PieceCases.AllPieces.Select(piece => (house, piece)));
    }
}