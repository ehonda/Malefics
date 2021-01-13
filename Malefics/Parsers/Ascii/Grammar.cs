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
            = Parse.Char(ROCK).Return(Tile.Rock());

        public static readonly Parser<Tile> Road
            = Parse.Char(ROAD).Return(Tile.Road());

        public static readonly Parser<Tile> Barricade
            = Parse.Char(BARRICADE).Return(Tile.Barricade());

        public static readonly Parser<Tile> RedPawn
            = Parse.Char(PAWN_RED).Return(Tile.Pawn(Player.Red));
    }
}
