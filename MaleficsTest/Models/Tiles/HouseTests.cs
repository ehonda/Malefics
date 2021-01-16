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
        public void Putting_A_Pawn_In_An_Empty_House_Occupies_It()
        {
            var house = Tile.House(Player.Red, 0);
            house.Put(new Pawn(Player.Red));

            Assert.That(house.IsOccupied(), Is.True);
        }

        [Test]
        public void Taking_From_A_Non_Empty_House_Retrieves_A_Pawn_Of_The_Houses_Player()
        {
            var house = Tile.House(Player.Red, 1);

            Assert.That(house.Take(), Is.EqualTo(new Pawn(Player.Red)));
        }
    }
}
