﻿using Malefics.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Extensions
{
    public static class Path
    {
        public static bool IsPath(this IEnumerable<Position> positions)
        {
            var positionsEnumerated = positions.ToArray();
            if (positionsEnumerated.Length < 2)
                return false;

            return positionsEnumerated
                .Zip(positionsEnumerated.Skip(1))
                .Aggregate(
                    true,
                    (IsPathUntilPq, pq) => IsPathUntilPq && (pq.First.IsNeighborOf(pq.Second)));
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
    }
}