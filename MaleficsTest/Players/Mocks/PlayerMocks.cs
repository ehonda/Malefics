using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CyclicEnumerables;
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
        {
            var mock = PlayerOfColor(playerColor);
            var moveEnumerator = moves.Cycle().GetEnumerator();

            mock
                .Setup(player => player.RequestPawnMove(
                    It.IsAny<Board>(),
                    It.IsAny<uint>()))
                .Returns(() =>
                {
                    moveEnumerator.MoveNext();
                    return moveEnumerator.Current;
                });

            return mock;
        }

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