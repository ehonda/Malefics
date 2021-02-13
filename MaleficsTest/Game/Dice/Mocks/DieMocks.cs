using CyclicEnumerables;
using Malefics.Game.Dice;
using Moq;
using System.Collections.Generic;

namespace MaleficsTests.Game.Dice.Mocks
{
    public static class DieMocks
    {
        public static Mock<IDie> Cyclic(IEnumerable<uint> rolls)
        {
            var mock = new Mock<IDie>();
            // TODO: Why do we get ReSharper warning about disposing enumerator, do we need to fix it?
            var rollEnumerator = rolls.Cycle().GetEnumerator();

            mock
                .Setup(die => die.Roll())
                .Returns(() =>
                {
                    rollEnumerator.MoveNext();
                    return rollEnumerator.Current;
                });

            return mock;
        }
    }
}
