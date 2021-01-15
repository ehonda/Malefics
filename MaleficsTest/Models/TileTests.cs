using Malefics.Enums;
using Malefics.Models;
using Malefics.Models.Pieces;
using NUnit.Framework;
using System;

namespace MaleficsTests.Models
{
    [TestFixture]
    public class TileTests
    {
        [Test]
        public void No_Piece_Can_Be_Added_To_A_Tile_With_Rock_Terrain()
        {
            var tile = Tile.Rock();
            Assert.Catch<InvalidOperationException>(
                () => tile.Add(new Barricade()));
        }

        [Test]
        public void A_Pawn_Can_Be_Added_To_An_Empty_House()
        {
            var house = new Tile { Terrain = Terrain.House };
            house.Add(new Pawn { Player = Player.Red });

            Assert.That(house.IsOccupied(), Is.True);
        }

        [Test]
        public void A_Pawn_Can_Be_Removed_From_A_House()
        {
            var house = Tile.House(Player.Red, 1);
            house.RemoveFirst();

            Assert.That(house.IsOccupied, Is.False);
        }
    }
}
