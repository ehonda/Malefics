using Malefics.Enums;
using Malefics.Models;
using Malefics.Models.Pieces;
using NUnit.Framework;
using System;
using Malefics.Models.Tiles;

namespace MaleficsTests.Models
{
    [TestFixture]
    public class TileTests
    {
        [Test]
        public void A_Pawn_Can_Be_Added_To_An_Empty_House()
        {
            var house = Tile.House(Player.Red, 0);
            house.Put(new Pawn(Player.Red));

            Assert.That((house as House)!.IsOccupied(), Is.True);
        }

        [Test]
        public void A_Pawn_Can_Be_Removed_From_A_House()
        {
            var house = Tile.House(Player.Red, 1);
            house.Take();

            Assert.That((house as House)!.IsOccupied, Is.False);
        }

        [Test]
        public void Taking_A_Piece_From_An_Empty_Road_Tile_Throws()
            => Assert.Catch<InvalidOperationException>(() => Tile.Road().Take());
    }
}
