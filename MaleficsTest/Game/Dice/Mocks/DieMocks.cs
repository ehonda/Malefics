using CyclicEnumerables;
using Malefics.Game.Dice;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MaleficsTests.Game.Dice.Mocks
{
    public static class DieMocks
    {
        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
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
