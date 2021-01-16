using System;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using NUnit.Framework;

namespace MaleficsTests.Models.Tiles
{
    [TestFixture]
    public class RockTests
    {
        [Test]
        public void No_Piece_Can_Be_Put_On_A_Tile_With_Rock_Terrain()
            => Assert.Catch<InvalidOperationException>(
                () => Tile.Rock().Put(new Barricade()));
    }
}
