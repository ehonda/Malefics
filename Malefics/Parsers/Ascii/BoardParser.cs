using Malefics.Models;
using Malefics.Models.Pieces;
using System;
using System.Linq;

namespace Malefics.Parsers.Ascii
{
    public class BoardParser
    {
        private readonly string ROW_END = "\n";
        private const char EMPTY_NODE = '.';
        private const char WALL = ' ';
        private const char BARRICADE = 'o';

        public Board Parse(string board)
        {
            var rows = board.Split(ROW_END);

            // TODO: Fix awkward null handling
            var nodePositions = rows
                .Reverse()
                .SelectMany((row, y) => row
                    .Select((node, x) => (new Position(x, y), ParseNode(node)))
                    .Where(positionAndNode => positionAndNode.Item2 != null));

            return new(nodePositions!);
        }

        private static Tile? ParseNode(char node)
            => node switch
            {
                EMPTY_NODE => Tile.Unoccupied(),
                WALL => null,
                BARRICADE => Tile.OccupiedBy(new Barricade()),
                _ => throw new ArgumentException($"Unkown node encoding: {node}")
            };
    }
}
