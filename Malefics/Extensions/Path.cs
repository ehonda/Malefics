using Malefics.Models;
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
    }
}
