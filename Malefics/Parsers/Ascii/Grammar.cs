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

        // Encodings
        // # # # # # # # # # # # # # # # # #

        private const char ROCK = ' ';
        private const char ROAD = '.';
        private const char BARRICADE = 'o';
        private const char PAWN_RED = 'r';
        private const char PAWN_BLUE = 'b';
        private const char HOUSE_RED = 'R';

        private static char PawnEncoding(Player player)
            => player switch
            {
                Player.Red => PAWN_RED,
                Player.Blue => PAWN_BLUE,
                _ => ' '
            };

        private static Parser<Tile> EncodedAs(this Tile tile, char encoding)
            => Parse.Char(encoding).Return(tile);

        // Simple terrain and pieces
        // # # # # # # # # # # # # # # # # #

        public static readonly Parser<Tile> Rock
            = Models.Tile.Rock().EncodedAs(ROCK);

        public static readonly Parser<Tile> Road
            = Models.Tile.Road().EncodedAs(ROAD);

        public static readonly Parser<Tile> Barricade
            = Models.Tile.Barricade().EncodedAs(BARRICADE);

        // Pawns
        // # # # # # # # # # # # # # # # # #

        public static readonly Parser<Tile> AnyPawn
            = Pawn(Player.Red).Or(Pawn(Player.Blue));

        public static Parser<Tile> Pawn(Player player)
            => Models.Tile.Pawn(player).EncodedAs(PawnEncoding(player));

        // Houses
        // # # # # # # # # # # # # # # # # #

        public static readonly Parser<Tile> RedHouse =
            from player in Parse.Char(HOUSE_RED)
            from pawns in Parse.Numeric
            select new Tile() { Terrain = Terrain.House };

        // Main parser
        // # # # # # # # # # # # # # # # # #

        public static readonly Parser<Tile> Tile
            = Rock
            .Or(Road)
            .Or(Barricade)
            .Or(AnyPawn)
            .Or(RedHouse);

        // Board Parser
        // ------------------------------------------------------------------

        public static readonly Parser<IEnumerable<Tile>> BoardRow =
            Tile.Until(Parse.LineTerminator);

        public static readonly Parser<Board> Board 
            = Parse.Many(BoardRow).Select(Models.Board.FromReversedTileRows);
    }
}
