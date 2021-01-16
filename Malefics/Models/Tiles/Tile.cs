using Malefics.Enums;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    // TODO: Convert to subclasses Road, Rock, etc. of ITile - Tests for subclasses!
    public static class Tile
    {
        public static ITile Rock() => new Rock();

        public static ITile Road() => new Road();

        public static ITile Barricade() => new Road(new Barricade());

        public static ITile Pawn(Player player) => new Road(new Pawn(player));

        public static ITile House(Player player, uint pawns)
            => new House(player, pawns);
    }
}
