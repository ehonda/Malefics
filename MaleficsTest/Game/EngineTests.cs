using Malefics.Enums;
using Malefics.Game;
using Malefics.Models;
using Malefics.Parsers.Ascii;
using MaleficsTests.Players.Mocks;
using Moq;
using NUnit.Framework;
using Sprache;
using System.Linq;
using Position = Malefics.Models.Position;

namespace MaleficsTests.Game
{
    [TestFixture]
    public class EngineTests
    {
        // TODO: Move duplicated code somewhere so EngineTests and BoardTests both use it
        private static Board FromRows(params string[] rows)
            => Grammar.Board.Parse(string.Join('\n', rows));

        [Test]
        public void The_Engine_Requests_Moves_From_Every_Player()
        {
            var red = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Red, Enumerable.Empty<Position>());
            var blue = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Blue, Enumerable.Empty<Position>());

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