using Malefics.Extensions;
using Malefics.Models;
using Malefics.Parsers.Ascii;
using NUnit.Framework;
using Sprache;

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
    }
}
