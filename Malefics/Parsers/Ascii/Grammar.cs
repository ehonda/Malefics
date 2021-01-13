using Malefics.Enums;
using Malefics.Models;
using Sprache;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Parsers.Ascii
{
    // TODO: Split up Tile/Board Parser into nicely named
    // (we don't want another 'Tile' in our namespace here)
    // static subclasses
    public static class Grammar
    {
        // Tile Parser
        // ------------------------------------------------------------------

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


        // Tile Parser
        // ------------------------------------------------------------------

        public static readonly Parser<IEnumerable<Tile>> BoardRow =
            Tile.Until(Parse.LineTerminator);
        //from tiles in Parse.Many(Tile)
        //from _ in Parse.String(ROW_END)
        //select tiles;

        public static readonly Parser<Board> Board 
            = Parse.Many(BoardRow).Select(rows => new Board(rows));
    }
}
