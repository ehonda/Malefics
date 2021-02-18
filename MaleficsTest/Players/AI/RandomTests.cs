using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Models;
using Malefics.Players.AI;
using MaleficsTests.Models;
using NUnit.Framework;

namespace MaleficsTests.Players.AI
{
    [TestFixture]
    public class RandomTests
    {
        private Board _board = new();
        private Random _player = new();

        [SetUp]
        public void SetUp()
        {
            _board = new();
            _player = new()
            {
                PlayerColor = PlayerColor.Red
            };
        }

        [Test]
        public void Random_Player_Chooses_From_Single_Legal_Pawn_move()
        {
            _board = ParseBoard.FromRows("r.");

            var move = _player.RequestPawnMove(_board, 1);

            Assert.That(move, Is.EquivalentTo(Path.AxisParallel(new(0, 0), new(1, 0))));
        }

        [Test]
        public void Random_Player_Chooses_From_Single_Legal_Barricade_Placement()
        {
            _board = ParseBoard.FromRows(".");

            var placement = _player.RequestBarricadePlacement(_board);

            Assert.That(placement, Is.EqualTo(new Position(0, 0)));
        }

        // TODO: More tests that a random element is chosen
    }
}