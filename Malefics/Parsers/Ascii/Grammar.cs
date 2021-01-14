using Malefics.Enums;
using Malefics.Models;
using Malefics.Models.Pieces;
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
        private const char HOUSE_RED = 'R';

        public static readonly Parser<Tile> Rock
            = Parse.Char(ROCK).Return(Models.Tile.Rock());

        public static readonly Parser<Tile> Road
            = Parse.Char(ROAD).Return(Models.Tile.Road());

        public static readonly Parser<Tile> Barricade
            = Parse.Char(BARRICADE).Return(Models.Tile.Barricade());

        public static readonly Parser<Tile> RedPawn
            = Parse.Char(PAWN_RED).Return(Models.Tile.Pawn(Player.Red));

        public static readonly Parser<Tile> RedHouse =
            from player in Parse.Char(HOUSE_RED)
            from pawns in Parse.Numeric
            select new Tile() { Terrain = Terrain.House };

        public static readonly Parser<Tile> Tile
            = Rock
            .Or(Road)
            .Or(Barricade)
            .Or(RedPawn)
            .Or(RedHouse);

        // Board Parser
        // ------------------------------------------------------------------

        public static readonly Parser<IEnumerable<Tile>> BoardRow =
            Tile.Until(Parse.LineTerminator);

        public static readonly Parser<Board> Board 
            = Parse.Many(BoardRow).Select(Models.Board.FromReversedTileRows);
    }
}
