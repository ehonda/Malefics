﻿using Malefics.Exceptions;
using Malefics.Extensions;
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
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPairs))]
        public void Putting_A_Piece_On_An_Occupied_Road_Tile_Throws((Piece, Piece) pieces)
            => Assert.Catch<InvalidTileOperationException>(
                () => new Road(pieces.First()).Put(pieces.Second()));

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void A_Road_With_A_Piece_Is_Occupied(Piece piece)
            => Assert.That(new Road(piece).IsOccupied, Is.True);

        [Test]
        public void A_Road_Occupied_By_A_Barricade_Is_Not_Traversable()
            => Assert.That(new Road(new Barricade()).IsTraversable, Is.False);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.Pawns))]
        public void A_Road_Occupied_By_A_Pawn_Is_Traversable(Pawn pawn)
            => Assert.That(new Road(pawn).IsTraversable, Is.True);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void An_Empty_Road_Is_Not_A_Valid_Capture_Target_For_Any_Piece(Piece piece)
            => Assert.That(new Road().IsValidCaptureTargetFor(piece), Is.False);

        [Test]
        [TestCase(null)]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void A_Road_Is_Never_A_Valid_Capture_Target_For_A_Barricade(Piece occupyingPiece)
            => Assert.That(new Road(occupyingPiece).IsValidCaptureTargetFor(new Barricade()), Is.False);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.PawnPairsOfDifferentColors))]
        public void A_Road_Occupied_By_A_Pawn_Is_A_Valid_Capture_Target_For_A_Pawn_Of_Another_Color(
            (Pawn, Pawn) occupierAndCapturer)
        {
            var (occupier, capturer) = occupierAndCapturer;
            var road = new Road(occupier);
            Assert.That(road.IsValidCaptureTargetFor(capturer), Is.True);
        }

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.Pawns))]
        public void A_Road_Occupied_By_A_Barricade_Is_A_Valid_Capture_Target_For_Any_Pawn(Pawn pawn)
            => Assert.That(new Road(new Barricade()).IsValidCaptureTargetFor(pawn), Is.True);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void A_Road_Occupied_By_A_Piece_Contains_That_Piece(Piece piece)
            => Assert.That(new Road(piece).Contains(piece), Is.True);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.Pawns))]
        public void A_Road_Occupied_By_A_Pawn_Returns_True_On_Contains_New_Pawn_Of_That_Color(
            Pawn pawn)
            => Assert.That(new Road(pawn).Contains(new Pawn(pawn.PlayerColor)), Is.True);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.UnequalPairs))]
        public void A_Road_Occupied_By_A_Piece_Does_Not_Contain_An_Unequal_Piece(
            (Piece, Piece) pieces)
            => Assert.That(new Road(pieces.First()).Contains(pieces.Second()), Is.False);

        [Test]
        public void Peeking_At_An_Unoccupied_Road_Returns_Null()
            => Assert.That(new Road().Peek(), Is.Null);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void Peeking_At_An_Occupied_Road_Returns_The_Occupying_Piece(Piece piece)
            => Assert.That(new Road(piece).Peek(), Is.EqualTo(piece));

        [Test]
        public void An_Unoccupied_Road_Is_Geometrically_Traversable()
            => Assert.That(new Road().IsGeometricallyTraversable(), Is.True);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void An_Occupied_Road_Is_Geometrically_Traversable(Piece piece)
            => Assert.That(new Road(piece).IsGeometricallyTraversable(), Is.True);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void Taking_From_A_Road_Unoccupies_It(Piece piece)
        {
            var road = new Road(piece);
            road.Take();
            Assert.That(road.IsOccupied, Is.False);
        }
    }
}
