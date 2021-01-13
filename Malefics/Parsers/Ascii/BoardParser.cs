using Malefics.Models;
using Sprache;
using System;
using System.Linq;

namespace Malefics.Parsers.Ascii
{
    public class BoardParser
    {
        private readonly string ROW_END = "\n";
        
        private readonly TileParser _tileParser = new();

        public Board Parse(string board)
        {
            //var rows = board.Split(ROW_END);

            //var rows = Grammar.BoardRow.Parse(board);

            //var rows = Sprache.Parse.Many(Grammar.BoardRow).Parse(board);

            //var nodePositions = rows
            //    .Reverse()
            //    .SelectMany((row, y) => row
            //        .Select((tile, x) => (new Models.Position(x, y), tile)));

            //return new(nodePositions);
            return Grammar.Board.Parse(board);
        }
    }
}
