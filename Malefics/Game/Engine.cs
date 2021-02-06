using System.Linq;
using Malefics.Game.Dice;
using Malefics.Game.MoveResults;
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
            while (true)
            {
                foreach (var player in _players)
                {
                    var roll = _die.Roll();
                    if (_board.PlayerCanMoveAPawn(player.PlayerColor, roll))
                    {
                        // TODO: Check that move path has length of roll - Or should Player guarantee it?
                        var move = player.RequestPawnMove(_board, roll);

                        var moveResult = _board.MovePawn(new(player.PlayerColor), move);
                        if (moveResult is Victory)
                            return player;
                    }
                }
            }
        }
    }
}
