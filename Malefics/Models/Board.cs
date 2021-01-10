using System.Collections.Generic;

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

        public bool IsUsable(Position position)
            => _nodes.ContainsKey(position);
    }
}
