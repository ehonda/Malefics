using Malefics.Extensions;
using Malefics.Models.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Models
{
    using Path = IEnumerable<Position>;

    public class Board
    {
        private readonly IDictionary<Position, Tile> _nodes
            = new Dictionary<Position, Tile>();

        public Board()
        {
        }

        public Board(IDictionary<Position, Tile> nodes)
            => _nodes = nodes;

        public Board(IEnumerable<(Position, Tile)> nodePositions)
            => _nodes = nodePositions.ToDictionary(Pair.First, Pair.Second);

        public Board(IEnumerable<IEnumerable<Tile>> rows)
            => _nodes = rows
                .Reverse()
                .SelectMany((row, y) => row
                    .Select((tile, x) => (new Position(x, y), tile)))
                .ToDictionary(Pair.First, Pair.Second);


        public bool IsTraversable(Position position)
            => _nodes.ContainsKey(position)
            && _nodes[position].Terrain is Terrain.Road
            && !_nodes[position].IsBarricaded();

        public bool IsLegalPath(Path path)
            => path.IsPath()
            && path.All(IsTraversable)
            && path.AllDistinct();

        public static Board FromReversedTileRows(IEnumerable<IEnumerable<Tile>> rows)
            => new(rows);
    }
}
