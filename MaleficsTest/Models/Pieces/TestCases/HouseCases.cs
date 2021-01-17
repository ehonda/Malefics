using System;
using System.Collections.Generic;
using System.Linq;
using Malefics.Enums;
using Malefics.Models.Tiles;

namespace MaleficsTests.Models.Pieces.TestCases
{
    public static class HouseCases
    {
        public static IEnumerable<House> AllEmptyHouses
            => Enum.GetValues<Player>().Select(player => new House(player, 0));
    }
}