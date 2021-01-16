using Malefics.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Models
{
    using Path = IEnumerable<Position>;

    public class Board
    {
        private readonly IDictionary<Position, ITile> _nodes
            = new Dictionary<Position, ITile>();

        public Board()
        {
        }

        public Board(IDictionary<Position, ITile> nodes)
            => _nodes = nodes;

        public Board(IEnumerable<(Position, ITile)> nodePositions)
            => _nodes = nodePositions.ToDictionary(Pair.First, Pair.Second);

        public Board(IEnumerable<IEnumerable<ITile>> rows)
            => _nodes = rows
                .Reverse()
                .SelectMany((row, y) => row
                    .Select((tile, x) => (new Position(x, y), tile)))
                .ToDictionary(Pair.First, Pair.Second);


        public bool IsTraversable(Position position)
            => _nodes.ContainsKey(position)
            && ((_nodes[position] as Tile)!).Terrain is Terrain.Road
            && !((_nodes[position] as Tile)!).IsBarricaded();

        public bool IsLegalPath(Path path)
            => path.IsPath()
            && path.All(IsTraversable)
            && path.AllDistinct();

        public static Board FromReversedTileRows(IEnumerable<IEnumerable<Tile>> rows)
            => new(rows);
    }
}
