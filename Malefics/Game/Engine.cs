using System.Linq;
using Malefics.Models;
using Malefics.Players;

namespace Malefics.Game
{
    public class Engine
    {
        private readonly Board _board;
        private readonly IPlayer[] _players;

        // TODO: Check players aren't empty
        // TODO: Check players are of different color
        public Engine(Board board, IPlayer[] players)
        {
            _board = board;
            _players = players;
        }

        public IPlayer Run()
        {
            foreach (var player in _players)
            {
                player.RequestPawnMove(_board, 0);
            }

            return _players.First();
        }
    }
}
