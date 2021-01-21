using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Malefics.Models;
using Malefics.Models.Tiles;

namespace Malefics.Extensions
{
    [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
    public static class Path
    {
        public static bool AllDistinct(this IEnumerable<Position> p)
        {
            var q = p.ToArray();
            return q.Distinct().Count() == q.Length;
        }

        public static IEnumerable<Position> AxisParallel(Position start, Position end)
        {
            if (start.X != end.X)
                return Enumerable
                    .Range(0, Math.Abs(start.X - end.X))
                    .Select(x => start.X < end.X ? x : -x)
                    .Select(x => new Position(start.X + x, start.Y))
                    .Append(end);

            if (start.Y != end.Y)
                return Enumerable
                    .Range(0, Math.Abs(start.Y - end.Y))
                    .Select(y => start.Y < end.Y ? y : -y)
                    .Select(y => new Position(start.X, start.Y + y))
                    .Append(end);

            throw new ArgumentException($"{start} and {end} have no equal coordinate");
        }

        public static IEnumerable<Position> AxisParallelSegments(params Position[] endpoints)
            => endpoints switch
            {
                { Length: < 2 } => throw new ArgumentException(
                    "Can't construct segments path from less than 2 endpoints."),

                _ => endpoints
                    .Zip(endpoints.Skip(1))
                    .Select(points => AxisParallel(points.First, points.Second))
                    .Aggregate(JoinPathTo)
            };

        // TODO: Re-implement board pawn legal move checking with this
        public static bool IsGeometricallyTraversablePath(
            this IEnumerable<(Position, ITile)> tilePath)
            => With.Array(
                tilePath,
                tilePath =>
                    tilePath
                        .Select(Pair.First)
                        .IsPath()
                    && tilePath
                        .Select(Pair.Second)
                        .All(tile => tile.IsGeometricallyTraversable()));

        public static bool IsPath(this IEnumerable<Position> positions)
            => With.Array(
                positions,
                positions =>
                    positions
                        .Zip(positions.Skip(1))
                        .Aggregate(
                            true,
                            (isPathUntilPq, pq) =>
                                isPathUntilPq
                                && pq.First.IsNeighborOf(pq.Second)));

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<Position> JoinPathTo(
            this IEnumerable<Position> p, IEnumerable<Position> q)
        {
            var p_ = p.ToArray();
            if (!p_.Any())
                return q;

            var q_0 = q.FirstOrDefault();
            if (q_0 is null)
                return p_;

            if (p_.Last() != q_0)
                throw new ArgumentException($"Can't join path ending in {p_.Last()} " +
                                            $"with path starting in {q_0}");

            return p_.Concat(q.Skip(1));
        }
    }
}