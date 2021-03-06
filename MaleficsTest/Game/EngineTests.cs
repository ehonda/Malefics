﻿using Malefics.Enums;
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
    // TODO: Is there a way to make setup for these tests less verbose?
    [TestFixture]
    [Timeout(5000)] // To prevent infinite Run() loop on erroneous test setup
    public class EngineTests
    {
        // TODO: Move duplicated code somewhere so EngineTests and BoardTests both use it
        private static Board FromRows(params string[] rows)
            => Grammar.Board().Parse(string.Join('\n', rows));

        [Test]
        public void The_Engine_Requests_Moves_From_Every_Player()
        {
            var red = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Red,
                Path.AxisParallel(new(0, 0), new(1, 0)));
            var blue = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Blue,
                Path.AxisParallel(new(6, 0), new(3, 0)));

            var engine = new Engine(
                FromRows("r..x..b"),
                new[] { red.Object, blue.Object },
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
                new[] { red.Object },
                DieMocks.Cyclic(new[] { 3u }).Object);

            var winner = engine.Run();

            Assert.That(winner.PlayerColor, Is.EqualTo(PlayerColor.Red));
        }

        [Test]
        public void Whoever_Moves_Onto_A_Goal_Tile_First_Wins()
        {
            var red = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Red,
                Path.AxisParallel(new(0, 0), new(2, 0)));

            var blue = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Blue,
                Path.AxisParallel(new(6, 0), new(3, 0)));

            var engine = new Engine(
                FromRows("r..x..b"),
                new[] { red.Object, blue.Object },
                DieMocks.Cyclic(new[] { 2u, 3u }).Object);

            var winner = engine.Run();

            Assert.That(winner.PlayerColor, Is.EqualTo(PlayerColor.Blue));
        }

        [Test]
        public void The_Engine_Does_Not_Request_A_Move_From_A_Player_That_Has_No_Moves()
        {
            var red = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Red,
                Enumerable.Empty<Position>());

            var blue = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Blue,
                Path.AxisParallel(new(6, 0), new(3, 0)));

            var engine = new Engine(
                FromRows("ro.x..b"),
                new[] { red.Object, blue.Object },
                DieMocks.Cyclic(new[] { 3u }).Object);

            engine.Run();

            red.Verify(p => p.RequestPawnMove(
                It.IsAny<Board>(),
                It.IsAny<uint>()), Times.Never);
        }

        [Test]
        public void Game_With_Multiple_Turns_Per_Player()
        {
            var red = PlayerMocks.CyclicMoveSequenceExecutor(
                PlayerColor.Red,
                new[]
                {
                    Path.AxisParallel(new(0, 0), new(1, 0)),
                    Path.AxisParallel(new(1, 0), new(2, 0))
                });

            var blue = PlayerMocks.CyclicMoveSequenceExecutor(
                PlayerColor.Blue,
                new[]
                {
                    Path.AxisParallel(new(7, 0), new(5, 0)),
                    Path.AxisParallel(new(5, 0), new(3, 0))
                });

            var engine = new Engine(
                FromRows("r..x...b"),
                new[] { red.Object, blue.Object },
                DieMocks.Cyclic(new[] { 1u, 2u }).Object);

            var winner = engine.Run();

            Assert.That(winner.PlayerColor, Is.EqualTo(PlayerColor.Blue));
        }

        [Test]
        public void Captured_Pawns_Are_Put_Back_Into_Houses()
        {
            var red = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Red,
                Path.AxisParallel(new(0, 0), new(1, 0)));

            var blue = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Blue,
                Path.AxisParallel(new(4, 0), new(3, 0)));

            var engine = new Engine(
                FromRows("rb xB0"),
                new[] { red.Object, blue.Object },
                DieMocks.Cyclic(new[] { 1u }).Object);

            var winner = engine.Run();

            Assert.That(winner.PlayerColor, Is.EqualTo(PlayerColor.Blue));
        }

        [Test]
        public void Place_Captured_Barricade()
        {
            var red = PlayerMocks.CyclicMoveSequenceExecutor(
                    PlayerColor.Red,
                    new[]
                    {
                        Path.AxisParallel(new(0, 0), new(1, 0)),
                        Path.AxisParallel(new(1, 0), new(2, 0))
                    })
                .WithCyclicBarricadePlacements(new[] { new Position(3, 0) });

            var blue = PlayerMocks.StaticPawnMoveExecutor(
                PlayerColor.Blue,
                Path.AxisParallel(new(4, 0), new(2, 0)));

            var engine = new Engine(
                FromRows("rox.b"),
                new[] { red.Object, blue.Object },
                DieMocks.Cyclic(new[] { 1u, 2u }).Object);

            var winner = engine.Run();

            Assert.That(winner.PlayerColor, Is.EqualTo(PlayerColor.Red));
        }
    }
}