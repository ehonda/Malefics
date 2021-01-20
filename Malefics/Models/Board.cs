using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using System.Collections.Generic;
using System.Linq;

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
                .SelectMany(positionAndTile => GetLegalMovePathsOfDistanceFrom(positionAndTile.Key, distance))
                .Any();

        private ITile TileAt(Position position)
            => _nodes.TryGetValue(position, out var tile)
                ? tile
                : Tile.Rock();

        public IEnumerable<IEnumerable<Position>> GetNonBacktrackingRoadPathsOfDistanceFrom(
            Position position, uint distance)
            => GetNonBacktrackingRoadPathsOfDistanceFrom(position, distance,
                    Enumerable.Empty<Position>());

        private IEnumerable<IEnumerable<Position>> GetNonBacktrackingRoadPathsOfDistanceFrom(
            Position position, uint distance, IEnumerable<Position> visited)
        {
            // TODO: Refactor to get rid of all the "tile is Road || tile is Goal"
            if (distance == 0u && TileAt(position) is Road || TileAt(position) is Goal)
                return new[] { new[] { position } };

            var roadNeighbors = position
                .Neighbors()
                .Where(neighbor
                    => (TileAt(neighbor) is Road || TileAt(neighbor) is Goal)
                       && !visited.Contains(neighbor));

            return roadNeighbors
                .SelectMany(neighbor =>
                    GetNonBacktrackingRoadPathsOfDistanceFrom(
                            neighbor, distance - 1, visited.Append(position))
                        .Select(path => path.Prepend(position)));
        }

        public IEnumerable<IEnumerable<Position>> GetLegalMovePathsOfDistanceFrom(
            Position position, uint distance)
        {
            var tile = TileAt(position);
            var pawnToMove = tile.Peek();
            if (pawnToMove is not Pawn)
                return Enumerable.Empty<IEnumerable<Position>>();

            return GetNonBacktrackingRoadPathsOfDistanceFrom(position, distance)
                .Where(path =>
                {
                    var pathAsArray = path.ToArray();
                    var lastTile = TileAt(pathAsArray.Last());
                    // We have to be able to
                    //      a - Capture at the last tile (or have it be unoccupied Road)
                    //      b - Traverse all previous tiles
                    return (lastTile.IsValidCaptureTargetFor(pawnToMove)
                           || (lastTile is Road && !lastTile.IsOccupied())
                           || lastTile is Goal)
                           && pathAsArray.Skip(1).Reverse().Skip(1).All(IsTraversable);
                });
        }

        public static Board FromReversedTileRows(IEnumerable<IEnumerable<ITile>> rows)
            => new(rows);
    }
}
