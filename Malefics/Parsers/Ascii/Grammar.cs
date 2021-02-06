using Malefics.Enums;
using Malefics.Models;
using Malefics.Models.Tiles;
using Sprache;
using System.Collections.Generic;

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
        private const char HOUSE_BLUE = 'B';
        private const char GOAL = 'x';

        private static char PawnEncoding(PlayerColor playerColor)
            => playerColor switch
            {
                PlayerColor.Red => PAWN_RED,
                PlayerColor.Blue => PAWN_BLUE,
                _ => ' '
            };

        private static char HouseEncoding(PlayerColor playerColor)
            => playerColor switch
            {
                PlayerColor.Red => HOUSE_RED,
                PlayerColor.Blue => HOUSE_BLUE,
                _ => ' '
            };

        private static Parser<ITile> EncodedAs(this ITile tile, char encoding)
            => Parse.Char(encoding).Return(tile);

        // Simple terrain and pieces
        // # # # # # # # # # # # # # # # # #

        private static Parser<ITile> Rock()
            => Models.Tiles.Tile.Rock().EncodedAs(ROCK);

        private static Parser<ITile> Road()
            => Models.Tiles.Tile.Road().EncodedAs(ROAD);

        private static Parser<ITile> Barricade()
            => Models.Tiles.Tile.Barricade().EncodedAs(BARRICADE);

        private static Parser<ITile> Goal()
            => new Goal().EncodedAs(GOAL);

        // Pawns
        // # # # # # # # # # # # # # # # # #

        private static Parser<ITile> AnyPawn()
            => Pawn(PlayerColor.Red).Or(Pawn(PlayerColor.Blue));

        private static Parser<ITile> Pawn(PlayerColor playerColor)
            => Models.Tiles.Tile.Pawn(playerColor).EncodedAs(PawnEncoding(playerColor));

        // Houses
        // # # # # # # # # # # # # # # # # #

        private static Parser<ITile> AnyHouse()
            => House(PlayerColor.Red).Or(House(PlayerColor.Blue));

        private static Parser<ITile> House(PlayerColor playerColor) =>
            from _ in Parse.Char(HouseEncoding(playerColor))
            from pawns in Parse.Numeric
            select Models.Tiles.Tile.House(playerColor, uint.Parse(pawns.ToString()));

        // Main parser
        // # # # # # # # # # # # # # # # # #

        public static Parser<ITile> Tile()
            => Rock()
                .Or(Road())
                .Or(Barricade())
                .Or(AnyPawn())
                .Or(AnyHouse())
                .Or(Goal());

        // Board Parser
        // ------------------------------------------------------------------

        private static Parser<IEnumerable<ITile>> BoardRow()
            => Tile().Until(Parse.LineTerminator);

        public static Parser<Board> Board()
            => BoardRow().Many().Select(Models.Board.FromReversedTileRows);
    }
}
