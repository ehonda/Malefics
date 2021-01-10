using Malefics.BoardReaders;
using NUnit.Framework;

namespace MaleficsTest.BoardReaders
{
    [TestFixture]
    public class AsciiReaderTest
    {
        private AsciiReader _reader;

        [SetUp]
        public void Setup()
        {
            _reader = new();
        }
    }
}
