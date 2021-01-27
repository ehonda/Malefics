using System.Linq;
using Malefics.Game.Dice;
using Malefics.Models;
using Malefics.Players;

namespace Malefics.Game
{
    public class Engine
    {
        private readonly Board _board;
        private readonly IPlayer[] _players;
        private readonly IDie _die;

        // TODO: Check players aren't empty
        // TODO: Check players are of different color
        public Engine(Board board, IPlayer[] players, IDie die)
        {
            _board = board;
            _players = players;
            _die = die;
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
