using Malefics.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Models
{
    using Path = IEnumerable<Position>;

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
            => _nodes = nodePositions.ToDictionary(Pair.First, Pair.Second);


        public bool IsUsable(Position position)
            => _nodes.ContainsKey(position);

        public bool IsLegalPath(Path path)
            => path.IsPath()
            && path.All(IsUsable);
    }
}
