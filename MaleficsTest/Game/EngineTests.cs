using Malefics.Enums;
using Malefics.Game;
using Malefics.Models;
using Malefics.Parsers.Ascii;
using MaleficsTests.Players.Mocks;
using Moq;
using NUnit.Framework;
using Sprache;
using System.Linq;
using Malefics.Extensions;
using MaleficsTests.Game.Dice.Mocks;
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

            var engine = new Engine(
                new(), 
                new[] {red.Object, blue.Object},
                DieMocks.Cyclic(new[] { 1u, 3u }).Object);
            
            engine.Run();

            red.Verify(p => p.RequestPawnMove(
                It.IsAny<Board>(),
                It.IsAny<uint>()), Times.AtLeastOnce);

            blue.Verify(p => p.RequestPawnMove(
                It.IsAny<Board>(),
                It.IsAny<uint>()), Times.AtLeastOnce);
        }

        [Test]
        public void Moving_Onto_A_Goal_Tile_Ends_The_Game()
        {
            var red = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Red,
                Path.AxisParallel(new(0, 0), new(3, 0)));

            var engine = new Engine(
                FromRows("r..x"),
                new[] {red.Object},
                DieMocks.Cyclic(new[] { 3u }).Object);

            var winner = engine.Run();

            Assert.That(winner.PlayerColor, Is.EqualTo(PlayerColor.Red));
        }
    }
}