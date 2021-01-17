using System.Collections.Generic;
using Malefics.Models;

namespace Malefics.Extensions
{
    public static class PositionExtensions
    {
        /// <summary>
        /// Uses the <see href="https://en.wikipedia.org/wiki/Von_Neumann_neighborhood"/>.
        /// </summary>
        /// <param name="p">Position to get the neighbors of.</param>
        /// <returns>The Von Neumann neighbors of <paramref name="p"/>.</returns>
        public static IEnumerable<Position> Neighbors(this Position p)
            => new[]
            {
                p with { X = p.X + 1 },
                p with { Y = p.Y - 1 },
                p with { X = p.X - 1 },
                p with { Y = p.Y + 1 }
            };
    }
}
