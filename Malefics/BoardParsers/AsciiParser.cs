using Malefics.Models;
using System;
using System.Linq;

namespace Malefics.BoardReaders
{
    public class AsciiParser
    {
        private readonly string ROW_END = "\n";
        private const char EMPTY_NODE = '.';

        public Board Parse(string board)
        {
            var rows = board.Split(ROW_END);

            var nodesWithPositions = rows
                .Reverse()
                .SelectMany((row, y) => row
                    .Select((node, x) => (ParseNode(node), new Position(x, y))));

            return new(nodesWithPositions.ToDictionary(np => np.Item2, np => np.Item1));
        }

        private static Node ParseNode(char node)
            => node switch
            {
                EMPTY_NODE => new(),
                _ => throw new ArgumentException($"Unkown node encoding: {node}")
            };
    }
}
