using Malefics.Enums;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using Malefics.Parsers.Ascii;
using NUnit.Framework;
using Sprache;

namespace MaleficsTests.Parsers.Ascii
{
    [TestFixture]
    public class TileParserTests
    {
        // TODO: Parameterize these tests

        [Test]
        public void Parse_Red_House_With_A_Pawn()
        {
            var tile = Grammar.Tile().Parse("R1");

            Assert.That(tile, Is.TypeOf<House>());
            Assert.That(tile.Contains(new Pawn(PlayerColor.Red)), Is.True);
        }

        // This is a regression test: We used to have our parsers be "static readonly"
        // properties of Grammar, which meant that their return values were only bound
        // once, and that modifying a value returned from one parse would lead to that
        // modified value being returned on subsequent parses.
        [Test]
        public void Parsing_A_Tile_And_Modifying_It_Does_Not_Affect_Subsequent_Parses()
        {
            var roadA = Grammar.Road().Parse(".");
            Assert.That(roadA.IsOccupied, Is.False);

            roadA.Put(new Barricade());

            var roadB = Grammar.Road().Parse(".");
            Assert.That(roadB.IsOccupied, Is.False);
        }

        [Test]
        public void A_Barricade_Is_Parsed_With_A_Road_Tile()
        {
            var tile = Grammar.Tile().Parse("o");

            Assert.That(tile, Is.TypeOf<Road>());
            Assert.That(tile.Contains(new Barricade()), Is.True);
        }

        [Test]
        public void A_Red_Pawn_Is_Parsed_With_A_Road_Tile()
        {
            var tile = Grammar.Tile().Parse("r");

            Assert.That(tile, Is.TypeOf<Road>());
            Assert.That(tile.Contains(new Pawn(PlayerColor.Red)), Is.True);
        }

        [Test]
        public void A_Blue_Pawn_Is_Parsed_With_A_Road_Tile()
        {
            var tile = Grammar.Tile().Parse("b");

            Assert.That(tile, Is.TypeOf<Road>());
            Assert.That(tile.Contains(new Pawn(PlayerColor.Blue)), Is.True);
        }

        [Test]
        public void Parse_Empty_Red_House()
        {
            var tile = Grammar.Tile().Parse("R0");

            Assert.That(tile, Is.TypeOf<House>());
            Assert.That(tile.Contains(new Pawn(PlayerColor.Red)), Is.False);
        }
    }
}
