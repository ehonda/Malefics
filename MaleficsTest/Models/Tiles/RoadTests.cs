using Malefics.Exceptions;
using Malefics.Models.Pieces;
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

        [Test]
        public void Putting_A_Piece_On_An_Occupied_Road_Tile_Throws()
            => Assert.Catch<InvalidTileOperationException>(
                () => new Road(new Barricade()).Put(new Barricade()));
    }
}
