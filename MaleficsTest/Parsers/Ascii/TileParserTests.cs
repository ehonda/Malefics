using Malefics.Enums;
using Malefics.Models;
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
        // TODO: We want a better way to test for expected tiles
        //       now that we made occupying pieces private

        // TODO: Reactivate the commented out tests

        // TODO: Parameterize these tests

        [Test]
        public void Parse_Red_House_With_A_Pawn()
        {
            var tile = Grammar.Tile().Parse("R1");

            Assert.That(tile, Is.TypeOf<House>());
            Assert.That(tile.Contains(new Pawn(PlayerColor.Red)), Is.True);
        }

        // This is a regression test: Before we returned generator functions
        // from our parsers, we returned tiles directly from a static context,
        // which meant we always returned a reference to the same tile. Thus,
        // modifying a tile returned on the first parse then yielded that modified
        // tile being returned on the second pass.
        [Test]
        public void Parsing_A_Tile_And_Modifying_It_Does_Not_Affect_Subsequent_Parses()
        {
            var roadA = Grammar.Road().Parse(".");
            Assert.That(roadA.IsOccupied, Is.False);

            roadA.Put(new Barricade());

            var roadB = Grammar.Road().Parse(".");
            Assert.That(roadB.IsOccupied, Is.False);
        }

        //[Test]
        //public void A_Barricade_Is_Parsed_With_A_Road_Tile()
        //{
        //    var tile = Grammar.Tile.Parse("o");

        //    Assert.That(tile.Terrain, Is.EqualTo(Terrain.Road));
        //    Assert.That(tile.OccupyingPieces, Has.Exactly(1).Items
        //        .And.Exactly(1).Items.TypeOf<Barricade>());
        //}

        //[Test]
        //public void A_Red_Pawn_Is_Parsed_With_A_Road_Tile()
        //{
        //    var tile = Grammar.Tile.Parse("r");

        //    Assert.That(tile.Terrain, Is.EqualTo(Terrain.Road));
        //    Assert.That(tile.OccupyingPieces, Has.Exactly(1).Items
        //        .And.Exactly(1).Items.TypeOf<Pawn>()
        //        .And.Matches<Pawn>(p => p.PlayerColor == PlayerColor.Red));
        //}

        //[Test]
        //public void A_Blue_Pawn_Is_Parsed_With_A_Road_Tile()
        //{
        //    var tile = Grammar.Tile.Parse("b");

        //    Assert.That(tile.Terrain, Is.EqualTo(Terrain.Road));
        //    Assert.That(tile.OccupyingPieces, Has.Exactly(1).Items
        //        .And.Exactly(1).Items.TypeOf<Pawn>()
        //        .And.Matches<Pawn>(p => p.PlayerColor == PlayerColor.Blue));
        //}

        //[Test]
        //public void Parse_Empty_Red_House()
        //{
        //    var tile = Grammar.Tile.Parse("R0");

        //    Assert.That(tile.Terrain, Is.EqualTo(Terrain.House));
        //    Assert.That(tile.OccupyingPieces, Is.Empty);
        //}
    }
}
