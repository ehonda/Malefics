using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Models;
using Malefics.Players;
using Moq;

namespace MaleficsTests.Players.Mocks
{
    public static class PlayerMocks
    {
        public static Mock<IPlayer> StaticPawnMoveExecutor(
            PlayerColor playerColor, IEnumerable<Position> move)
            => CyclicMoveSequenceExecutor(playerColor, new[] { move });

        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
        public static Mock<IPlayer> CyclicMoveSequenceExecutor(
            PlayerColor playerColor, IEnumerable<IEnumerable<Position>> moves)
            => With.Array(
                moves,
                moves =>
                {
                    if (moves.Length == 0)
                        throw new ArgumentException(
                            "Can't construct cyclic move executor from empty move sequence.");

                    var mock = PlayerOfColor(playerColor);

                    // TODO: Refactor such that cyclic die and cyclic move executor reuse common logic
                    var moveEnumerator = moves.GetEnumerator();
                    moveEnumerator.MoveNext();

                    mock
                        .Setup(player => player.RequestPawnMove(
                            It.IsAny<Board>(),
                            It.IsAny<uint>()))
                        .Returns(() =>
                        {
                            var move = moveEnumerator.Current;
                            if (!moveEnumerator.MoveNext())
                            {
                                moveEnumerator = moves.GetEnumerator();
                                moveEnumerator.MoveNext();
                            }

                            // TODO: This is kinda awkward, is there a better way to do this?
                            return (IEnumerable<Position>) move!;
                        });

                    return mock;
                });

        private static Mock<IPlayer> PlayerOfColor(PlayerColor playerColor)
        {
            var mock = new Mock<IPlayer>();
            mock
                .Setup(player => player.PlayerColor)
                .Returns(playerColor);
            return mock;
        }
    }
}