using Malefics.BoardReaders;
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
        public void Undeclared_Node_Is_Unusable()
        {
            var asciiBoard = "";
            var board = _parser.Parse(asciiBoard);
            Assert.False(board.IsUsable(new(0, 0)));
        }

        [Test]
        public void Empty_Node_Is_Usable()
        {
            var asciiBoard = ".";
            var board = _parser.Parse(asciiBoard);
            Assert.True(board.IsUsable(new(0, 0)));
        }
    }
}
