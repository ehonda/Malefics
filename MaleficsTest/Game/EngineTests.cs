using Malefics.Game;
using Malefics.Models;
using Malefics.Players;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace MaleficsTests.Game
{
    [TestFixture]
    public class EngineTests
    {
        [Test]
        public void The_Engine_Requests_Moves_From_Every_Player()
        {
            var red = new Mock<IPlayer>();
            var blue = new Mock<IPlayer>();

            red.Setup(p => p.RequestPawnMove(
                    It.IsAny<Board>(),
                    It.IsAny<uint>()))
                .Returns(Enumerable.Empty<Position>());

            blue.Setup(p => p.RequestPawnMove(
                    It.IsAny<Board>(),
                    It.IsAny<uint>()))
                .Returns(Enumerable.Empty<Position>());

            var engine = new Engine(new(), new[] {red.Object, blue.Object});
            engine.Run();

            red.Verify(p => p.RequestPawnMove(
                It.IsAny<Board>(),
                It.IsAny<uint>()), Times.AtLeastOnce);

            blue.Verify(p => p.RequestPawnMove(
                It.IsAny<Board>(),
                It.IsAny<uint>()), Times.AtLeastOnce);
        }
    }
}