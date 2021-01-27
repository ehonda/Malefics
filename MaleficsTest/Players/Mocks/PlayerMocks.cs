using System.Collections.Generic;
using Malefics.Enums;
using Malefics.Models;
using Malefics.Players;
using Moq;

namespace MaleficsTests.Players.Mocks
{
    public static class PlayerMocks
    {
        public static Mock<IPlayer> StaticPawnMoveExecutor(
            PlayerColor playerColor, IEnumerable<Position> move)
        {
            var mock = new Mock<IPlayer>();

            mock
                .Setup(player => player.RequestPawnMove(
                    It.IsAny<Board>(),
                    It.IsAny<uint>()))
                .Returns(move);

            mock
                .Setup(player => player.PlayerColor)
                .Returns(playerColor);

            return mock;
        }
    }
}