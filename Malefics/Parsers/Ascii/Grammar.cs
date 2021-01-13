using Malefics.Models;
using Sprache;

namespace Malefics.Parsers.Ascii
{
    public static class Grammar
    {
        public static readonly Parser<Tile> Rock
            = Parse.Char(' ').Return(Tile.Rock());

        public static readonly Parser<Tile> Road
            = Parse.Char('.').Return(Tile.Road());

        public static readonly Parser<Tile> Barricade
            = Parse.Char('o').Return(Tile.Barricade());
    }
}
