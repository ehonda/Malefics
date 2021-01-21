using System;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Extensions
{
    public static class With
    {
        public static S Array<T, S>(IEnumerable<T> ts, Func<T[], S> f) => f(ts.ToArray());
    }
}
