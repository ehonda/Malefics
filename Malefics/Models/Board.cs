using System.Collections.Generic;
using System.Linq;

namespace Malefics.Models
{
    public class Board
    {
        private readonly IDictionary<Position, Node> _nodes
            = new Dictionary<Position, Node>();

        public Board()
        {
        }

        public Board(IDictionary<Position, Node> nodes)
            => _nodes = nodes;

        public Board(IEnumerable<(Position, Node)> nodePositions)
            => _nodes = nodePositions.ToDictionary(
                First,
                Second);

        private T First<T, S>((T, S) pair) => pair.Item1;
        private S Second<T, S>((T, S) pair) => pair.Item2;


        public bool IsUsable(Position position)
            => _nodes.ContainsKey(position);
    }
}
