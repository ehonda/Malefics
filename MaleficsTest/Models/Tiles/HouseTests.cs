using Malefics.Enums;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using NUnit.Framework;

namespace MaleficsTests.Models.Tiles
{
    [TestFixture]
    public class HouseTests
    {
        [Test]
        public void A_Pawn_Can_Be_Added_To_An_Empty_House()
        {
            var house = Tile.House(Player.Red, 0);
            house.Put(new Pawn(Player.Red));

            Assert.That(house.IsOccupied(), Is.True);
        }

        [Test]
        public void A_Pawn_Can_Be_Removed_From_A_House()
        {
            var house = Tile.House(Player.Red, 1);
            house.Take();

            Assert.That(house.IsOccupied, Is.False);
        }
    }
}
