using Malefics.Enums;
using Malefics.Models;
using Malefics.Models.Pieces;
using Malefics.Parsers.Ascii;
using NUnit.Framework;

namespace MaleficsTest.Parsers.Ascii
{
    [TestFixture]
    public class TileParserTests
    {
        private TileParser _parser = new();

        [SetUp]
        public void Setup()
        {
            _parser = new();
        }

        [Test]
        public void A_Barricade_Is_Parsed_With_A_Road_Tile()
        {
            var tile = _parser.Parse('o');
            Assert.That(tile, Has.Property(nameof(Tile.Terrain)).EqualTo(Terrain.Road));
            Assert.That(tile, Has.Property(nameof(Tile.OccupyingPiece))
                .TypeOf(new Barricade().GetType()));
        }

        [Test]
        public void A_Red_Pawn_Is_Parsed_With_A_Road_Tile()
        {
            var tile = _parser.Parse('r');
            Assert.That(tile, Has.Property(nameof(Tile.Terrain)).EqualTo(Terrain.Road));
            Assert.That(tile.OccupyingPiece, Is.InstanceOf<Pawn>()
                .And.Matches<Pawn>(p => p.Player == Player.Red));
        }
    }
}
