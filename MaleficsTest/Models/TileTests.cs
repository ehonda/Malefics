using Malefics.Enums;
using Malefics.Models;
using Malefics.Models.Pieces;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MaleficsTests.Models
{
    [TestFixture]
    public class TileTests
    {
        //[Test]
        //public void No_Piece_Can_Be_Added_To_A_Tile_With_Rock_Terrain()
        //{
        //    var tile = Tile.Rock();
        //    Assert.Catch<InvalidOperationException>(
        //        () => tile.OccupyingPieces.Add(new Barricade()));
        //}

        [Test]
        public void A_Pawn_Can_Be_Added_To_An_Empty_House()
        {
            var house = new Tile { Terrain = Terrain.House };
            house.OccupyingPieces.Add(new Pawn { Player = Player.Red });

            Assert.That(house.OccupyingPieces, Has.Exactly(1).Items);
        }

        [Test]
        public void A_Pawn_Can_Be_Removed_From_A_House()
        {
            var house = new Tile 
            {
                Terrain = Terrain.House, 
                OccupyingPieces = new[] { new Pawn { Player = Player.Red } }
            };
            house.OccupyingPieces.RemoveAt(0);

            Assert.That(house.OccupyingPieces, Is.Empty);
        }
    }
}
