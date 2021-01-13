using Malefics.Enums;
using Malefics.Models;
using Sprache;

namespace Malefics.Parsers.Ascii
{
    public static class Grammar
    {
        private const char ROCK = ' ';
        private const char ROAD = '.';
        private const char BARRICADE = 'o';
        private const char PAWN_RED = 'r';

        public static readonly Parser<Tile> Rock
            = Parse.Char(ROCK).Return(Models.Tile.Rock());

        public static readonly Parser<Tile> Road
            = Parse.Char(ROAD).Return(Models.Tile.Road());

        public static readonly Parser<Tile> Barricade
            = Parse.Char(BARRICADE).Return(Models.Tile.Barricade());

        public static readonly Parser<Tile> RedPawn
            = Parse.Char(PAWN_RED).Return(Models.Tile.Pawn(Player.Red));

        public static readonly Parser<Tile> Tile
            = Rock
            .Or(Road)
            .Or(Barricade)
            .Or(RedPawn);
    }
}
