using Malefics.Models;
using System;
using System.Linq;

namespace Malefics.Parsers
{
    public class AsciiParser
    {
        private readonly string ROW_END = "\n";
        private const char EMPTY_NODE = '.';

        public Board Parse(string board)
        {
            var rows = board.Split(ROW_END);

            var nodePositions = rows
                .Reverse()
                .SelectMany((row, y) => row
                    .Select((node, x) => (new Position(x, y), ParseNode(node))));

            return new(nodePositions);
        }

        private static Node ParseNode(char node)
            => node switch
            {
                EMPTY_NODE => new(),
                _ => throw new ArgumentException($"Unkown node encoding: {node}")
            };
    }
}
