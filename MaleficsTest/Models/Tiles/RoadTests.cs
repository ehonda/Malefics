using Malefics.Enums;
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

        [Test]
        public void A_Road_With_A_Piece_Is_Occupied()
            => Assert.That(new Road(new Barricade()).IsOccupied, Is.True);

        [Test]
        public void A_Road_Occupied_By_A_Barricade_Is_Not_Traversable()
            => Assert.That(new Road(new Barricade()).IsTraversable, Is.False);

        [Test]
        public void A_Road_Occupied_By_A_Pawn_Is_Traversable()
            => Assert.That(new Road(new Pawn(Player.Red)).IsTraversable, Is.True);
    }
}
