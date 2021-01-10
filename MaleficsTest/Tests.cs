using Malefics.Models;
using NUnit.Framework;

namespace MaleficsTest
{
    public class Tests
    {
        private Board _board;

        [SetUp]
        public void Setup()
        {
            _board = new();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
