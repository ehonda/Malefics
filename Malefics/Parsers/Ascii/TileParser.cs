using Malefics.Models;
using Sprache;

namespace Malefics.Parsers.Ascii
{
    public class TileParser
    {
        public Tile Parse(char tile)
            => Grammar.Tile.Parse(tile.ToString());
    }
}
