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

        public static ITile Pawn(Player player) => new Road(new Pawn(player));

        public static ITile House(Player player, uint pawns)
            => new House(player, pawns);
    }
}
