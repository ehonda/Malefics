﻿using Malefics.Enums;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public static class Tile
    {
        // TODO: Do we really need these static constructor functions
        //       or should we replace them by direct constructor calls?
        public static ITile Rock() => new Rock();

        public static ITile Road() => new Road();

        public static ITile Barricade() => new Road(new Barricade());

        public static ITile Pawn(PlayerColor playerColor) => new Road(new Pawn(playerColor));

        public static ITile House(PlayerColor playerColor, uint pawns)
            => new House(playerColor, pawns);

        // TODO: Should we expose these "higher order predicates" via interface or extension?
        // TODO: Dedicated unit tests
        public static bool AllowsMovingOver(this ITile tile)
            => tile switch
            {
                Tiles.Road => !tile.Contains(new Barricade()),
                Goal => true,
                _ => false
            };

        public static bool AllowsBeingLandedOnBy(this ITile tile, Pawn pawn)
            => tile switch
            {
                Tiles.Road => !tile.Contains(pawn),
                Goal => true,
                _ => false
            };

        public static Piece CaptureWith(this ITile tile, Pawn pawn)
        {
            var captured = tile.Take();
            tile.Put(pawn);
            return captured;
        }
    }
}
