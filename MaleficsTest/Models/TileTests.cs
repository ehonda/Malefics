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
        //[Test]
        //public void No_Piece_Can_Be_Added_To_A_Tile_With_Rock_Terrain()
        //{
        //    var tile = Tile.Rock();
        //    Assert.Catch<InvalidOperationException>(
        //        () => tile.Add(new Barricade()));
        //}

        [Test]
        public void A_Pawn_Can_Be_Added_To_An_Empty_House()
        {
            var house = Tile.House(Player.Red, 0);
            house.Add(new Pawn(Player.Red));

            Assert.That((house as House)!.IsOccupied(), Is.True);
        }

        [Test]
        public void A_Pawn_Can_Be_Removed_From_A_House()
        {
            var house = Tile.House(Player.Red, 1);
            (house as House)!.Remove(new Pawn(Player.Red));

            Assert.That((house as House)!.IsOccupied, Is.False);
        }

        //[Test]
        //public void Removing_A_Piece_From_An_Empty_Tile_Throws()
        //{
        //    var tile = Tile.Rock();
        //    Assert.Catch<InvalidOperationException>(
        //        () => tile.Remove(new Barricade()));
        //}
    }
}
