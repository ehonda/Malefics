using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;

namespace Malefics.Models
{
    public class Board
    {
        private readonly IDictionary<Position, ITile> _tiles
            = new Dictionary<Position, ITile>();

        public Board()
        {
        }

        private Board(IEnumerable<IEnumerable<ITile>> rows)
            => _tiles = rows
                .Reverse()
                .SelectMany((row, y) => row
                    .Select((tile, x) => (new Position(x, y), tile)))
                .ToDictionary(Pair.First, Pair.Second);

        public static Board FromReversedTileRows(IEnumerable<IEnumerable<ITile>> rows)
            => new(rows);

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
                            || lastTile is Road && !lastTile.IsOccupied()
                            || lastTile is Goal)
                           && pathAsArray.Skip(1).Reverse().Skip(1).All(IsTraversable);
                });
        }

        public IEnumerable<IEnumerable<Position>> GetNonBacktrackingRoadPathsOfDistanceFrom(
            Position position, uint distance)
            => GetNonBacktrackingRoadPathsOfDistanceFrom(position, distance,
                Enumerable.Empty<Position>());

        private IEnumerable<IEnumerable<Position>> GetNonBacktrackingRoadPathsOfDistanceFrom(
            Position position, uint distance, IEnumerable<Position> visited)
        {
            // TODO: Refactor to get rid of all the "tile is Road || tile is Goal"
            if (distance == 0u && TileAt(position) is Road || TileAt(position) is Goal)
                return new[] {new[] {position}};

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


        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
        public bool IsLegalPawnMovePath(IEnumerable<Position> path, Pawn pawn)
            => With.Array(
                With.Array(path, path => path.Zip(path.Select(TileAt))),
                tilePath =>
                    tilePath.First().Second.Contains(pawn)
                    && tilePath.Last().Second.AllowsBeingLandedOnBy(pawn)
                    && tilePath.IsGeometricallyTraversablePath()
                    && tilePath.Inner().All(tilePosition => tilePosition.Second.AllowsMovingOver()));

        public bool IsLegalMovePath(IEnumerable<Position> path)
        {
            var pathAsArray = path.ToArray();
            return pathAsArray.IsPath()
                   && pathAsArray.All(IsTraversable)
                   && pathAsArray.AllDistinct();
        }

        public bool IsTraversable(Position position)
            => _tiles.ContainsKey(position)
               && _tiles[position].IsTraversable();

        public bool PlayerCanMoveAPawn(Player player, uint distance)
            => _tiles
                .Where(positionAndTile => positionAndTile.Value.Contains(new Pawn(player)))
                .SelectMany(positionAndTile => GetLegalMovePathsOfDistanceFrom(positionAndTile.Key, distance))
                .Any();

        private ITile TileAt(Position position)
            => _tiles.TryGetValue(position, out var tile)
                ? tile
                : Tile.Rock();
    }
}