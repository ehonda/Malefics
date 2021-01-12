using Malefics.Models;
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
            var rows = board.Split(ROW_END);

            var nodePositions = rows
                .Reverse()
                .SelectMany((row, y) => row
                    .Select((node, x) => (new Position(x, y), _tileParser.Parse(node))));

            return new(nodePositions);
        }
    }
}
