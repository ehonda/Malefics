using Malefics.Models;
using Sprache;

namespace Malefics.Parsers.Ascii
{
    public static class Grammar
    {
        public static readonly Parser<Tile> Rock
            = Parse.Char(' ').Return(new Tile() { Terrain = Terrain.Rock });
    }
}
