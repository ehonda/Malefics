using MaleficsTests.Game.Dice.Mocks;
using NUnit.Framework;

namespace MaleficsTests.Game.Dice
{
    [TestFixture]
    public class DieTests
    {
        [Test]
        public void Cyclic_Die_Mock()
        {
            var die = DieMocks.Cyclic(new[] {1u, 2u}).Object;

            Assert.That(die.Roll(), Is.EqualTo(1u));
            Assert.That(die.Roll(), Is.EqualTo(2u));
            Assert.That(die.Roll(), Is.EqualTo(1u));
            Assert.That(die.Roll(), Is.EqualTo(2u));
        }
    }
}
