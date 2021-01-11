using Malefics.Extensions;
using Malefics.Models;
using Malefics.Parsers;
using NUnit.Framework;

namespace MaleficsTest
{
    [TestFixture]
    public class BoardTest
    {
        private Board _board = new();
        private AsciiParser _parser = new();

        [SetUp]
        public void Setup()
        {
            _board = new();
            _parser = new();
        }

        [Test]
        public void A_Straight_Path_Over_Unoccupied_Usable_Nodes_Is_Valid()
        {
            _board = _parser.Parse("..");
            Assert.True(_board.IsValidPath(Path.AxisParallel(new(0, 0), new(1, 0))));
        }

        [Test]
        public void A_Straight_Path_Over_An_Unusable_Node_Is_Not_Valid()
        {
            _board = _parser.Parse(". .");
            Assert.False(_board.IsValidPath(Path.AxisParallel(new(0, 0), new(2, 0))));
        }
    }
}
