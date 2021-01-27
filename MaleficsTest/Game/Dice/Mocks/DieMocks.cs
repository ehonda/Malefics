using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Malefics.Extensions;
using Malefics.Game.Dice;
using Moq;

namespace MaleficsTests.Game.Dice.Mocks
{
    public static class DieMocks
    {
        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
        public static Mock<IDie> Cyclic(IEnumerable<uint> rolls)
            => With.Array(
                rolls,
                rolls =>
                {
                    if (rolls.Length == 0)
                        throw new ArgumentException(
                            "Can't construct cyclic die mock from empty pip sequence.");

                    var mock = new Mock<IDie>();

                    // TODO: Implement more cleanly
                    var pip = rolls.GetEnumerator();
                    pip.MoveNext();

                    mock
                        .Setup(die => die.Roll())
                        .Returns(() =>
                        {
                            var roll = pip.Current;
                            if (!pip.MoveNext())
                            {
                                pip = rolls.GetEnumerator();
                                pip.MoveNext();
                            }

                            return (uint)roll;
                        });

                    return mock;
                });
    }
}
