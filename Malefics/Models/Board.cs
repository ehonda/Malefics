using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

        public IEnumerable<IEnumerable<Position>> GetLegalPawnMovePathsOfDistanceFrom(
            Position position, uint distance)
            => TileAt(position).Peek() switch
            {
                Pawn pawn => GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(position, distance)
                    .Where(path => IsLegalPawnMovePath(path, pawn)),

                _ => Enumerable.Empty<IEnumerable<Position>>()
            };

        public IEnumerable<IEnumerable<Position>> GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(
            Position position, uint distance)
            => GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(position, distance,
                Enumerable.Empty<Position>());

        private IEnumerable<IEnumerable<Position>> GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(
            Position position, uint distance, IEnumerable<Position> visited)
        {
            // TODO: Refactor to get rid of all the "tile is Road || tile is Goal"
            if (distance == 0u && TileAt(position).IsGeometricallyTraversable())
                return new[] {new[] {position}};

            var roadNeighbors = position
                .Neighbors()
                .Where(neighbor
                    => (TileAt(neighbor) is Road || TileAt(neighbor) is Goal)
                       && !visited.Contains(neighbor));

            return roadNeighbors
                .SelectMany(neighbor =>
                    GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(
                            neighbor, distance - 1, visited.Append(position))
                        .Select(path => path.Prepend(position)));
        }

        // TODO: There's a bug here, we don't require the path to be non-backtracking!
        //       - Fix it and write a test
        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
        public bool IsLegalPawnMovePath(IEnumerable<Position> path, Pawn pawn)
            => With.Array(
                With.Array(path, path => path.Zip(path.Select(TileAt))),
                tilePath =>
                    tilePath.First().Second.Contains(pawn)
                    && tilePath.Last().Second.AllowsBeingLandedOnBy(pawn)
                    && tilePath.IsGeometricallyTraversablePath()
                    && tilePath.Select(Pair.First).IsNonBacktracking()
                    && tilePath.Inner().All(tilePosition => tilePosition.Second.AllowsMovingOver()));

        public bool IsTraversable(Position position)
            => _tiles.ContainsKey(position)
               && _tiles[position].IsTraversable();

        public bool PlayerCanMoveAPawn(Player player, uint distance)
            => _tiles
                .Where(positionAndTile => positionAndTile.Value.Contains(new Pawn(player)))
                .SelectMany(positionAndTile => GetLegalPawnMovePathsOfDistanceFrom(positionAndTile.Key, distance))
                .Any();

        private ITile TileAt(Position position)
            => _tiles.TryGetValue(position, out var tile)
                ? tile
                : Tile.Rock();
    }
}