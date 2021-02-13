using CyclicEnumerables;
using Malefics.Enums;
using Malefics.Models;
using Malefics.Players;
using Moq;
using System.Collections.Generic;

namespace MaleficsTests.Players.Mocks
{
    public static class PlayerMocks
    {
        public static Mock<IPlayer> StaticPawnMoveExecutor(
            PlayerColor playerColor, IEnumerable<Position> move)
            => CyclicMoveSequenceExecutor(playerColor, new[] { move });

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

        public static Mock<IPlayer> WithCyclicBarricadePlacements(
            this Mock<IPlayer> mock, IEnumerable<Position> placements)
        {
            var placementsEnumerator = placements.Cycle().GetEnumerator();

            mock
                .Setup(player =>
                    player.RequestBarricadePlacement(It.IsAny<Board>()))
                .Returns(() =>
                {
                    placementsEnumerator.MoveNext();
                    return placementsEnumerator.Current;
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