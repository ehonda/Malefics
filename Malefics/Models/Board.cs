using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Game.MoveResults;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Malefics.Models
{
    // TODO: Should this implement Renderable directly, or have an associated view?
    public class Board : Renderable
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

        // TODO: Custom exception type, better error message (print path)
        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
        public MoveResult MovePawn(Pawn pawn, IEnumerable<Position> path)
            => With.Array<Position, MoveResult>(
                path,
                path =>
                {
                    // TODO: Better implementation! This works, but doesn't feel as concise as it could be
                    if (!IsLegalPawnMovePath(path, pawn))
                        throw new InvalidOperationException($"Can't move {pawn} along illegal path");

                    var destination = TileAt(path.Last());

                    if (destination is Goal)
                        return new Victory(pawn.PlayerColor);

                    // Must be Road otherwise
                    if (!destination.IsOccupied())
                    {
                        destination.Put(pawn);
                        return new TurnFinished();
                    }

                    return new PieceCaptured(destination.CaptureWith(pawn));
                });

        public void AddPawnToPlayerHouse(Pawn pawn)
        {
            // ReSharper disable once VariableHidesOuterVariable
            var house = _tiles.Values
                .SingleOrDefault(tile => tile is House house && house.PlayerColor == pawn.PlayerColor);

            // TODO: Custom exception type
            if (house is null)
                throw new InvalidOperationException($"No house to put {pawn} in on board.");

            house.Put(pawn);
        }

        public IEnumerable<IEnumerable<Position>> GetLegalPawnMovesOfDistanceForPlayer(
            PlayerColor playerColor, uint distance)
            => _tiles
                .Where(positionAndTile => positionAndTile.Value.Contains(new Pawn(playerColor)))
                .SelectMany(positionAndTile => GetLegalPawnMovePathsOfDistanceFrom(positionAndTile.Key, distance));

        public IEnumerable<IEnumerable<Position>> GetLegalPawnMovePathsOfDistanceFrom(
            Position position, uint distance)
            => TileAt(position).Peek() switch
            {
                Pawn pawn => GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(position, distance)
                    .Where(path => IsLegalPawnMovePath(path, pawn)),

                _ => Enumerable.Empty<IEnumerable<Position>>()
            };

        private IEnumerable<IEnumerable<Position>> GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(
            Position position, uint distance)
            => GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(position, distance,
                Enumerable.Empty<Position>());

        private IEnumerable<IEnumerable<Position>> GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(
            Position position, uint distance, IEnumerable<Position> visited)
            => distance switch
            {
                0u => TileAt(position).IsGeometricallyTraversable()
                    ? new[] {new[] {position}}
                    : Enumerable.Empty<IEnumerable<Position>>(),

                _ => position
                    .Neighbors()
                    .Where(neighbor
                        => TileAt(neighbor).IsGeometricallyTraversable()
                           && !visited.Contains(neighbor))
                    .SelectMany(neighbor =>
                        GetNonBacktrackingGeometricallyTraversablePathsOfDistanceFrom(
                                neighbor, distance - 1, visited.Append(position))
                            .Select(path => path.Prepend(position)))
            };

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

        public bool PlayerCanMoveAPawn(PlayerColor playerColor, uint distance)
            => GetLegalPawnMovesOfDistanceForPlayer(playerColor, distance).Any();

        public bool IsLegalBarricadePlacement(Position position)
            => TileAt(position) is Road road
               && road.IsOccupied() is false;

        public void PlaceBarricade(Position position)
        {
            if (IsLegalBarricadePlacement(position) is false)
                // TODO: Custom exception, better error message (reason)
                throw new InvalidOperationException($"Can't place barricade at {position}");

            TileAt(position).Put(new Barricade());
        }

        private ITile TileAt(Position position)
            => _tiles.TryGetValue(position, out var tile)
                ? tile
                : Tile.Rock();

        #region Overrides of Renderable

        /// <inheritdoc />
        protected override IEnumerable<Segment> Render(RenderContext context, int maxWidth)
            => _tiles
                .GroupBy(kv => kv.Key.Y)
                .OrderByDescending(rowGroup => rowGroup.Key)
                .SelectMany(rowGroup => rowGroup
                    .OrderBy(kv => kv.Key.X)
                    .SelectMany(kv => kv.Value.Render(context, maxWidth))
                    .Concat((new Markup(Environment.NewLine) as IRenderable).Render(context, maxWidth)));

        #endregion
    }
}