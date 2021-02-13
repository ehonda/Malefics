using System;
using System.Collections;
using System.Collections.Generic;

namespace CyclicEnumerables
{
    // TODO: Publish this whole project as a nuget, use the nuget in MaleficsTests
    public class CyclicEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _base;

        public CyclicEnumerator(IEnumerator<T> @base) => _base = @base;

        /// <inheritdoc />
        public bool MoveNext()
        {
            if (_base.MoveNext() is false)
            {
                Reset();
                if (_base.MoveNext() is false) return false;
            }

            Current = _base.Current;
            return true;
        }

        /// <inheritdoc />
        public void Reset()
        {
            _base.Reset();
        }

        // CS8766 is wrong here - IEnumerator<T>.Current has [CanBeNull]
        /// <inheritdoc />
        public T? Current { get; private set; }

        /// <inheritdoc />
        object? IEnumerator.Current => Current;

        /// <inheritdoc />
        public void Dispose()
        {
            _base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}