using System;
using System.Linq;
using Malefics.Enums;
using Malefics.Exceptions;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using MaleficsTests.Models.Pieces.TestCases;
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

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.All))]
        public void An_Empty_Road_Is_Not_A_Valid_Capture_Target_For_Any_Piece(IPiece piece)
            => Assert.That(new Road().IsValidCaptureTargetFor(piece), Is.False);

        [Test]
        [TestCase(null)]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.All))]
        public void A_Road_Is_Never_A_Valid_Capture_Target_For_A_Barricade(IPiece occupyingPiece)
            => Assert.That(new Road(occupyingPiece).IsValidCaptureTargetFor(new Barricade()), Is.False);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.Pawns))]
        public void A_Road_Occupied_By_A_Pawn_Is_A_Valid_Capture_Target_For_A_Pawn_Of_Another_Color(
            Pawn occupyingPiece)
        {
            var road = new Road(occupyingPiece);
            var pawnsOfDifferentColorsCanCapture = Enum
                .GetValues<Player>()
                .Where(player => player != occupyingPiece.Player)
                .Select(player => road.IsValidCaptureTargetFor(new Pawn(player)));

            Assert.That(pawnsOfDifferentColorsCanCapture, Is.All.False);
        }
    }
}
