using Malefics.Parsers.Ascii;
using NUnit.Framework;

namespace MaleficsTest.Parsers.Ascii
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
        public void The_Empty_String_Is_Parsed_As_All_Rock_Tiles()
        {
            var asciiBoard = "";
            var board = _parser.Parse(asciiBoard);
            Assert.False(board.IsTraversable(new(0, 0)));
        }

        [Test]
        public void An_Unoccupied_Road_Tile_Is_Traversable()
        {
            var asciiBoard = ".";
            var board = _parser.Parse(asciiBoard);
            Assert.True(board.IsTraversable(new(0, 0)));
        }
    }
}
