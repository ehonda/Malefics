using Malefics.Models;
using System;
using System.Linq;

namespace Malefics.Parsers
{
    public class AsciiParser
    {
        private readonly string ROW_END = "\n";
        private const char EMPTY_NODE = '.';
        private const char WALL = ' ';

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

        private static Node? ParseNode(char node)
            => node switch
            {
                EMPTY_NODE => new(),
                WALL => null,
                _ => throw new ArgumentException($"Unkown node encoding: {node}")
            };
    }
}
