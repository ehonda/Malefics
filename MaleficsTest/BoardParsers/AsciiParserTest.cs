using Malefics.Parsers;
using NUnit.Framework;

namespace MaleficsTest.BoardReaders
{
    [TestFixture]
    public class AsciiParserTest
    {
        private AsciiParser _parser = new();

        [SetUp]
        public void Setup()
        {
            _parser = new();
        }

        [Test]
        public void Undeclared_Node_Is_Not_Traversable()
        {
            var asciiBoard = "";
            var board = _parser.Parse(asciiBoard);
            Assert.False(board.IsTraversable(new(0, 0)));
        }

        [Test]
        public void Empty_Node_Is_Traversable()
        {
            var asciiBoard = ".";
            var board = _parser.Parse(asciiBoard);
            Assert.True(board.IsTraversable(new(0, 0)));
        }
    }
}
