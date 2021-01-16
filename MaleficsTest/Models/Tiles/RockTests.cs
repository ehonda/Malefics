﻿using Malefics.Exceptions;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using NUnit.Framework;

namespace MaleficsTests.Models.Tiles
{
    [TestFixture]
    public class RockTests
    {
        [Test]
        public void No_Piece_Can_Be_Put_On_A_Rock_Tile()
            => Assert.Catch<InvalidTileOperationException>(
                () => Tile.Rock().Put(new Barricade()));

        [Test]
        public void A_Rock_Can_Not_Be_Taken_From()
            => Assert.Catch<InvalidTileOperationException>(
                () => Tile.Rock().Take());

        [Test]
        public void A_Rock_Tile_Is_Not_Traversable()
            => Assert.That(Tile.Rock().IsTraversable, Is.False);

        [Test]
        public void A_Rock_Is_Never_Occupied()
            => Assert.That(Tile.Rock().IsOccupied, Is.False);
    }
}
