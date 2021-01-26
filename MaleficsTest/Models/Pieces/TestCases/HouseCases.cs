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
            => AllHousesWithPopulation(0);

        public static IEnumerable<House> AllHousesWithSinglePawn
            => AllHousesWithPopulation(1);

        public static IEnumerable<House> AllHousesWithPopulation(uint pawns)
            => Enum.GetValues<PlayerColor>().Select(player => new House(player, pawns));
    }
}