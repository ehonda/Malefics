using Malefics.Extensions;
using System.Collections.Generic;
using System.Linq;
using Malefics.Enums;
using Malefics.Models.Tiles;

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

        private Board(IEnumerable<IEnumerable<ITile>> rows)
            => _nodes = rows
                .Reverse()
                .SelectMany((row, y) => row
                    .Select((tile, x) => (new Position(x, y), tile)))
                .ToDictionary(Pair.First, Pair.Second);


        public bool IsTraversable(Position position)
            => _nodes.ContainsKey(position) 
               && _nodes[position].IsTraversable();

        public bool IsLegalPath(Path path)
        {
            var pathAsArray = path.ToArray();
            return pathAsArray.IsPath()
                   && pathAsArray.All(IsTraversable)
                   && pathAsArray.AllDistinct();
        }

        //public bool PlayerCanMoveAPawn(Player player, uint distance)
        //    => _nodes.Values.Where(tile => tile is Road);

        public static Board FromReversedTileRows(IEnumerable<IEnumerable<ITile>> rows)
            => new(rows);
    }
}
