﻿using Malefics.Enums;
using Malefics.Models;
using Malefics.Models.Pieces;
using Malefics.Parsers.Ascii;
using NUnit.Framework;
using Sprache;

namespace MaleficsTest.Parsers.Ascii
{
    [TestFixture]
    public class TileParserTests
    {
        [Test]
        public void A_Barricade_Is_Parsed_With_A_Road_Tile()
        {
            var tile = Grammar.Tile.Parse("o");
            
            Assert.That(tile.Terrain, Is.EqualTo(Terrain.Road));
            Assert.That(tile.OccupyingPieces, Has.Exactly(1).Items
                .And.Exactly(1).Items.TypeOf<Barricade>());
        }

        [Test]
        public void A_Red_Pawn_Is_Parsed_With_A_Road_Tile()
        {
            var tile = Grammar.Tile.Parse("r");

            Assert.That(tile.Terrain, Is.EqualTo(Terrain.Road));
            Assert.That(tile.OccupyingPieces, Has.Exactly(1).Items
                .And.Exactly(1).Items.TypeOf<Pawn>()
                .And.Matches<Pawn>(p => p.Player == Player.Red));
        }

        [Test]
        public void A_Blue_Pawn_Is_Parsed_With_A_Road_Tile()
        {
            var tile = Grammar.Tile.Parse("b");

            Assert.That(tile.Terrain, Is.EqualTo(Terrain.Road));
            Assert.That(tile.OccupyingPieces, Has.Exactly(1).Items
                .And.Exactly(1).Items.TypeOf<Pawn>()
                .And.Matches<Pawn>(p => p.Player == Player.Blue));
        }

        [Test]
        public void Parse_Empty_Red_House()
        {
            var tile = Grammar.Tile.Parse("R0");

            Assert.That(tile.Terrain, Is.EqualTo(Terrain.House));
            Assert.That(tile.OccupyingPieces, Is.Empty);
        }
    }
}
