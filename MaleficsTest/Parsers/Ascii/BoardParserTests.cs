using Malefics.Parsers.Ascii;
using NUnit.Framework;
using Sprache;

namespace MaleficsTest.Parsers.Ascii
{
    [TestFixture]
    public class BoardParserTests
    {
        [Test]
        public void The_Empty_String_Is_Parsed_As_All_Rock_Tiles()
        {
            var asciiBoard = "";
            var board = Grammar.Board.Parse(asciiBoard);
            Assert.False(board.IsTraversable(new(0, 0)));
        }

        [Test]
        public void An_Unoccupied_Road_Tile_Is_Traversable()
        {
            var asciiBoard = ".";
            var board = Grammar.Board.Parse(asciiBoard);
            Assert.True(board.IsTraversable(new(0, 0)));
        }
    }
}
