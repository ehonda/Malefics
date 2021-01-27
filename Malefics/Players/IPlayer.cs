using System.Collections.Generic;
using Malefics.Enums;
using Malefics.Models;

namespace Malefics.Players
{
    public interface IPlayer
    {
        public PlayerColor PlayerColor { get; init; }

        // TODO: Replace Board access by something like a "view" access
        public IEnumerable<Position> RequestPawnMove(Board board, uint pips);
    }
}
