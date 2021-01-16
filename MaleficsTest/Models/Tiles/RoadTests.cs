using Malefics.Exceptions;
using Malefics.Models.Tiles;
using NUnit.Framework;

namespace MaleficsTests.Models.Tiles
{
    [TestFixture]
    public class RoadTests
    {
        [Test]
        public void Taking_A_Piece_From_An_Empty_Road_Tile_Throws()
            => Assert.Catch<InvalidTileOperationException>(
                () => Tile.Road().Take());
    }
}
