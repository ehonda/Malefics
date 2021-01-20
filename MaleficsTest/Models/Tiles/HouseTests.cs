using Malefics.Enums;
using Malefics.Exceptions;
using Malefics.Extensions;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using MaleficsTests.Models.Pieces.TestCases;
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
        [TestCaseSource(typeof(HouseCases), nameof(HouseCases.AllHousesWithPopulation),
            new object[] { 0u })]
        [TestCaseSource(typeof(HouseCases), nameof(HouseCases.AllHousesWithPopulation),
            new object[] { 1u })]
        public void A_House_Is_Not_Traversable(House house)
            => Assert.That(house.IsTraversable(), Is.False);

        [Test]
        [TestCaseSource(typeof(HouseCases), nameof(HouseCases.AllHousesWithPopulation), 
            new object[] { 0u })]
        [TestCaseSource(typeof(HouseCases), nameof(HouseCases.AllHousesWithPopulation),
            new object[] { 1u })]
        [TestCaseSource(typeof(HouseCases), nameof(HouseCases.AllHousesWithPopulation),
            new object[] { 2u })]
        public void A_Pawn_Can_Be_Put_In_A_House_Of_The_Right_Color_And_It_Is_Then_Occupied(
            House house)
        {
            house.Put(new Pawn(house.Player));
            Assert.That(house.IsOccupied(), Is.True);
        }

        [Test]
        [TestCaseSource(typeof(MixedCases), nameof(MixedCases.AllEmptyHouses_PawnsOfDifferentColor))]
        public void A_Pawn_Can_Not_Be_Put_Into_A_House_Of_Another_Color(
            (House, Pawn) houseAndPawn)
        {
            var (house, pawn) = houseAndPawn;
            Assert.Catch<InvalidTileOperationException>(() => house.Put(pawn));
        }

        [Test]
        [TestCaseSource(typeof(HouseCases), nameof(HouseCases.AllEmptyHouses))]
        public void A_Barricade_Can_Not_Be_Put_Into_A_House(House house)
            => Assert.Catch<InvalidTileOperationException>(() => house.Put(new Barricade()));

        [Test]
        [TestCaseSource(typeof(HouseCases), nameof(HouseCases.AllHousesWithSinglePawn))]
        public void Taking_From_A_Non_Empty_House_Retrieves_A_Pawn_Of_The_Houses_Color(House house)
            => Assert.That(house.Take(), Is.EqualTo(new Pawn(house.Player)));

        [Test]
        [TestCaseSource(typeof(HouseCases), nameof(HouseCases.AllEmptyHouses))]
        public void Taking_From_An_Empty_House_Throws(House house)
            => Assert.Catch<InvalidTileOperationException>(() => house.Take());

        [Test]
        [TestCaseSource(typeof(MixedCases), nameof(MixedCases.AllEmptyHouses_AllPieces))]
        public void A_House_Is_Not_A_Valid_Capture_Target_For_Any_Piece(
            (House, Piece) houseAndPiece)
            => Assert.That(houseAndPiece.First()
                .IsValidCaptureTargetFor(houseAndPiece.Second()), Is.False);

        // TODO: Find a nicer way to handle mixed cases, parameterize these tests
        //       to test all possible combinations
        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void An_Empty_House_Does_Not_Contain_Any_Piece(Piece piece)
            => Assert.That(new House(Player.Red, 0).Contains(piece), Is.False);

        [Test]
        public void A_Non_Empty_House_Contains_A_Pawn_Of_That_Color()
            => Assert.That(new House(Player.Red, 1).Contains(new Pawn(Player.Red)), Is.True);

        [Test]
        public void A_Non_Empty_House_Does_Not_Contain_A_Pawn_Of_Another_Color()
            => Assert.That(new House(Player.Red, 1).Contains(new Pawn(Player.Blue)), Is.False);

        [Test]
        public void A_Non_Empty_House_Does_Not_Contain_A_Barricade()
            => Assert.That(new House(Player.Red, 1).Contains(new Barricade()), Is.False);
    }
}
