using Malefics.Models;
using Malefics.Players;

namespace Malefics.Game
{
    public class Engine
    {
        private readonly Board _board;
        private readonly IPlayer[] _players;

        public Engine(Board board, IPlayer[] players)
        {
            _board = board;
            _players = players;
        }

        public void Run()
        {
            foreach (var player in _players)
            {
                player.RequestPawnMove(_board, 0);
            }
        }
    }
}
