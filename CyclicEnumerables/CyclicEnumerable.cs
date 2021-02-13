using System.Collections;
using System.Collections.Generic;

namespace CyclicEnumerables
{
    public class CyclicEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _base;

        public CyclicEnumerable(IEnumerable<T> @base) => _base = @base;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => new CyclicEnumerator<T>(_base.GetEnumerator());

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}