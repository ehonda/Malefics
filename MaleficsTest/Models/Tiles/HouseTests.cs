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
        public void An_Empty_House_Is_Not_Occupied()
            => Assert.That(new House(Player.Red, 0).IsOccupied(), Is.False);

        [Test]
        [TestCase(0u)]
        [TestCase(1u)]
        [TestCase(2u)]
        public void A_House_Is_Not_Traversable(uint pawns)
            => Assert.That(new House(Player.Red, pawns).IsTraversable(), Is.False);

        [Test]
        [TestCase(0u)]
        [TestCase(1u)]
        [TestCase(2u)]
        public void A_Pawn_Can_Be_Put_In_A_House_Of_The_Right_Color_And_It_Is_Then_Occupied(
            uint pawnsInHouse)
        {
            var house = Tile.House(Player.Red, pawnsInHouse);
            house.Put(new Pawn(Player.Red));

            Assert.That(house.IsOccupied(), Is.True);
        }

        [Test]
        public void Taking_From_A_Non_Empty_House_Retrieves_A_Pawn_Of_The_Houses_Color()
        {
            var house = Tile.House(Player.Red, 1);

            Assert.That(house.Take(), Is.EqualTo(new Pawn(Player.Red)));
        }
    }
}
