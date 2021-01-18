using System.Collections.Generic;
using System.Linq;
using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Models;
using Malefics.Parsers.Ascii;
using NUnit.Framework;
using Sprache;
using Position = Malefics.Models.Position;

namespace MaleficsTests.Models
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board = new();

        private static Board FromRows(params string[] rows)
            => Grammar.Board.Parse(string.Join('\n', rows));

        [SetUp]
        public void Setup()
        {
            _board = new();
        }

        [Test]
        public void A_Straight_Path_Over_Unoccupied_Road_Tiles_Is_Legal()
        {
            _board = FromRows("..");
            Assert.True(_board.IsLegalPath(Path.AxisParallel(new(0, 0), new(1, 0))));
        }

        [Test]
        public void A_Straight_Path_Over_A_Rock_Tile_Is_Not_Legal()
        {
            _board = FromRows(". .");
            Assert.False(_board.IsLegalPath(Path.AxisParallel(new(0, 0), new(2, 0))));
        }

        [Test]
        public void A_Path_Over_Road_Tiles_Around_A_Corner_Is_Legal()
        {
            _board = FromRows(
                "  .",
                "  .",
                "...");

            Assert.True(_board.IsLegalPath(
                Path.AxisParallelSegments(new(0, 0), new(2, 0), new(2, 2))));
        }

        [Test]
        public void A_Path_Passing_Over_A_Barricade_Is_Not_Legal()
        {
            _board = FromRows("..o.");

            Assert.False(_board.IsLegalPath(
                Path.AxisParallel(new(0, 0), new(0, 3))));
        }

        [Test]
        public void A_Path_Over_Road_Tiles_That_Backtracks_Is_Not_Legal()
        {
            _board = FromRows("...");

            Assert.False(_board.IsLegalPath(
                Path.AxisParallelSegments(new(0, 0), new(2, 0), new(1, 0))));
        }

        [Test]
        public void Non_Backtracking_Paths_Of_Distance_2_On_A_Square()
        {
            _board = FromRows(
                "..",
                "..");

            var paths = _board
                .GetNonBacktrackingRoadPathsOfDistanceFrom(new(0, 0), 2)
                .ToArray();

            Assert.That(paths, Has.Length.EqualTo(2));
            Assert.That(paths, Has.Exactly(1).Items.EquivalentTo(
                new[] { new Position(0, 0), new(1, 0), new(1, 1)}));
            Assert.That(paths, Has.Exactly(1).Items.EquivalentTo(
                new[] { new Position(0, 0), new(0, 1), new(1, 1) }));
        }

        // TODO: Test for barricade
        [Test]
        public void There_Are_No_Legal_Move_Paths_From_A_Tile_Without_A_Pawn()
        {
            _board = FromRows("...");

            var paths = _board
                .GetLegalMovePathsOfDistanceFrom(new(0, 0), 2)
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
                .GetLegalMovePathsOfDistanceFrom(new(0, 0), 3)
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
                .GetLegalMovePathsOfDistanceFrom(new(0, 0), 2)
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

            Assert.That(_board.PlayerCanMoveAPawn(Player.Red, 3), Is.True);
        }
    }
}
