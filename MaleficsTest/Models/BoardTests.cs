using System;
using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Models;
using Malefics.Parsers.Ascii;
using NUnit.Framework;
using Sprache;
using System.Linq;
using Malefics.Game.MoveResults;
using Malefics.Models.Pieces;
using Position = Malefics.Models.Position;

namespace MaleficsTests.Models
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board = new();

        private static Board FromRows(params string[] rows)
            => Grammar.Board().Parse(string.Join('\n', rows));

        [SetUp]
        public void Setup()
        {
            _board = new();
        }

        // TODO: Test for barricade
        [Test]
        public void There_Are_No_Legal_Move_Paths_From_A_Tile_Without_A_Pawn()
        {
            _board = FromRows("...");

            var paths = _board
                .GetLegalPawnMovePathsOfDistanceFrom(new(0, 0), 2)
                .ToArray();

            Assert.That(paths, Is.Empty);
        }

        [Test]
        public void One_Path_Is_Blocked_And_One_Path_Is_Legal()
        {
            _board = FromRows(
                ".",
                "o",
                ".",
                "r...");

            var paths = _board
                .GetLegalPawnMovePathsOfDistanceFrom(new(0, 0), 3)
                .ToArray();

            Assert.That(paths, Has.Length.EqualTo(1));
            Assert.That(paths, Has.Exactly(1).Items.EquivalentTo(
                new[] { new Position(0, 0), new(1, 0), new(2, 0), new(3, 0) }));
        }

        [Test]
        public void Legal_Move_Path_With_Barricade_Capture()
        {
            _board = FromRows("r.o");

            var paths = _board
                .GetLegalPawnMovePathsOfDistanceFrom(new(0, 0), 2)
                .ToArray();

            Assert.That(paths, Has.Length.EqualTo(1));
            Assert.That(paths, Has.Exactly(1).Items.EquivalentTo(
                new[] { new Position(0, 0), new(1, 0), new(2, 0) }));
        }

        // TODO: Test cases
        //      - r...r (4)
        //      - r...o (4)
        //      - r...b (4)
        //      - start from house

        [Test]
        public void A_Player_Has_A_Pawn_And_Can_Move_It_Over_A_Valid_Path()
        {
            _board = FromRows("r...");

            Assert.That(_board.PlayerCanMoveAPawn(PlayerColor.Red, 3), Is.True);
        }

        [Test]
        public void Capturing_A_Barricade_Is_A_Legal_Move_Path()
        {
            _board = FromRows("r..o");

            Assert.That(_board.PlayerCanMoveAPawn(PlayerColor.Red, 3), Is.True);
        }

        [Test]
        public void Capturing_Another_Pawn_Is_A_Legal_Move_Path()
        {
            _board = FromRows("r..b");

            Assert.That(_board.PlayerCanMoveAPawn(PlayerColor.Red, 3), Is.True);
        }

        [Test]
        public void Moving_Out_Of_The_House_Is_A_Legal_Move_Path()
        {
            _board = FromRows(
                "...",
                "R1");

            Assert.That(_board.PlayerCanMoveAPawn(PlayerColor.Red, 3), Is.True);
        }

        [Test]
        public void Landing_On_A_House_Is_Not_A_Legal_Move_Path()
        {
            _board = FromRows(
                "..r",
                "R0");

            Assert.That(_board.PlayerCanMoveAPawn(PlayerColor.Red, 3), Is.False);
        }

        [Test]
        public void Landing_On_A_Goal_Tile_Is_A_Legal_Move_Path()
        {
            _board = FromRows("r..x");

            Assert.That(_board.PlayerCanMoveAPawn(PlayerColor.Red, 3), Is.True);
        }

        // TODO: Extract to separate class
        // IsLegalPawnMovePath tests
        // -----------------------------------------------------------------------

        // Unit tests
        // Landing tile cases
        // r...
        // r..r
        // r..o
        // r..b
        // r..x
        // r..R
        //
        // Moving over tile cases
        // r. .
        // r.r.
        // r.o.
        // r.b.
        // r.x.
        // r.R.
        // r.B.

        // Landing tile cases
        // ----------------------------------------------------------------

        [Test]
        public void Moving_A_Red_Pawn_To_Land_On_An_Empty_Road_Is_Legal()
            => Assert.That(
                FromRows("r...")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)), 
                Is.True);

        [Test]
        public void Moving_A_Red_Pawn_To_Land_On_A_Red_Pawn_Is_Not_Legal()
            => Assert.That(
                FromRows("r..r")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.False);

        [Test]
        public void Moving_A_Red_Pawn_To_Land_On_A_Barricade_Is_Legal()
            => Assert.That(
                FromRows("r..o")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.True);

        [Test]
        public void Moving_A_Red_Pawn_To_Land_On_A_Blue_Pawn_Is_Legal()
            => Assert.That(
                FromRows("r..b")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.True);

        [Test]
        public void Moving_A_Red_Pawn_To_Land_On_A_Goal_Is_Legal()
            => Assert.That(
                FromRows("r..x")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.True);

        [Test]
        public void Moving_A_Red_Pawn_To_Land_On_A_House_Is_Not_Legal()
            => Assert.That(
                FromRows("r..R")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.False);

        // Moving over tile cases
        // ----------------------------------------------------------------

        [Test]
        public void Moving_A_Red_Pawn_Over_A_Rock_Is_Not_Legal()
            => Assert.That(
                FromRows("r. .")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.False);

        [Test]
        public void Moving_A_Red_Pawn_Over_A_Red_Pawn_Is_Legal()
            => Assert.That(
                FromRows("r.r.")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.True);

        [Test]
        public void Moving_A_Red_Pawn_Over_A_Barricade_Is_Not_Legal()
            => Assert.That(
                FromRows("r.o.")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.False);

        [Test]
        public void Moving_A_Red_Pawn_Over_A_Blue_Pawn_Is_Legal()
            => Assert.That(
                FromRows("r.b.")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.True);

        [Test]
        public void Moving_A_Red_Pawn_Over_A_Goal_Is_Legal()
            => Assert.That(
                FromRows("r.x.")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.True);

        [Test]
        public void Moving_A_Red_Pawn_Over_A_Red_House_Is_Not_Legal()
            => Assert.That(
                FromRows(
                        "  .",
                        "r.R0")
                    .IsLegalPawnMovePath(
                        Path.AxisParallelSegments(new(0, 0), new(2, 0), new(2, 1)),
                        new(PlayerColor.Red)),
                Is.False);

        [Test]
        public void Moving_A_Red_Pawn_Over_A_Blue_House_Is_Not_Legal()
            => Assert.That(
                FromRows(
                        "  .",
                        "r.B0")
                    .IsLegalPawnMovePath(
                        Path.AxisParallel(new(0, 0), new(3, 0)),
                        new(PlayerColor.Red)),
                Is.False);

        // Backtracking
        // ----------------------------------------------------------------

        [Test]
        public void Moving_A_Red_Pawn_Along_A_Backtracking_Path_Is_Not_Legal()
            => Assert.That(
                FromRows("r..")
                    .IsLegalPawnMovePath(
                        Path.AxisParallelSegments(new(0, 0), new(2, 0), new(1, 0)),
                        new(PlayerColor.Red)),
                Is.False);

        // TODO: Extract to separate class
        // MovePawn tests
        // -----------------------------------------------------------------------

        [Test]
        public void Moving_A_Pawn_Along_An_Illegal_Path_Throws()
            => Assert.Catch<InvalidOperationException>(
                () => FromRows("r..")
                    .MovePawn(
                        new(PlayerColor.Red),
                        Path.AxisParallel(new(0, 0), new(0, 2))));

        [Test]
        public void Moving_A_Pawn_Onto_An_Empty_Road_Tile_Returns_TurnFinished()
            => Assert.That(
                FromRows("r..")
                    .MovePawn(
                        new(PlayerColor.Red),
                        Path.AxisParallel(new(0, 0), new(2, 0))),
                Is.EqualTo(new TurnFinished()));

        [Test]
        public void Moving_A_Pawn_Onto_A_Barricade_Returns_BarricadeCapture()
            => Assert.That(
                FromRows("r.o")
                    .MovePawn(
                        new(PlayerColor.Red),
                        Path.AxisParallel(new(0, 0), new(2, 0))),
                Is.EqualTo(new BarricadeCaptured()));
    }
}
