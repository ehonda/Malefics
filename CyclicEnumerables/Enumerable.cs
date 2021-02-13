using System.Collections.Generic;

namespace CyclicEnumerables
{
    public static class Enumerable
    {
        public static CyclicEnumerable<T> Cycle<T>(this IEnumerable<T> xs) => new(xs);
    }
}