using Malefics.Extensions;
using System.Collections.Generic;
using System.Linq;
using Malefics.Enums;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;

namespace Malefics.Models
{
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

        public bool IsLegalPath(IEnumerable<Position> path)
        {
            var pathAsArray = path.ToArray();
            return pathAsArray.IsPath()
                   && pathAsArray.All(IsTraversable)
                   && pathAsArray.AllDistinct();
        }

        public bool PlayerCanMoveAPawn(Player player, uint distance)
            => _nodes
                .Where(positionAndTile => positionAndTile.Value.Contains(new Pawn(player)))
                .SelectMany(positionAndTile => GetPathsOfDistanceFrom(positionAndTile.Key, distance))
                .Any(IsLegalPath);

        private IEnumerable<IEnumerable<Position>> GetPathsOfDistanceFrom(
            Position position, uint distance)
            => distance switch
            {
                0u => new[] {Path.AxisParallel(position, position)},
                _ => position
                    .Neighbors()
                    .SelectMany(p => GetPathsOfDistanceFrom(p, distance - 1))
                    .Select(path => path.Prepend(position))
            };

        public static Board FromReversedTileRows(IEnumerable<IEnumerable<ITile>> rows)
            => new(rows);
    }
}
