using System.Collections.Generic;
using System.Linq;
using Malefics.Enums;
using Malefics.Extensions;
using Malefics.Models;

namespace Malefics.Players.AI
{
    public class Random : IPlayer
    {
        private readonly System.Random _rng = new();

        #region Implementation of IPlayer

        /// <inheritdoc />
        public PlayerColor PlayerColor { get; init; }

        /// <inheritdoc />
        public IEnumerable<Position> RequestPawnMove(Board board, uint pips)
            => With.Array(
                board.GetLegalPawnMovesOfDistanceForPlayer(PlayerColor, pips),
                moves => moves.ElementAt(_rng.Next(moves.Length)));

        /// <inheritdoc />
        public Position RequestBarricadePlacement(Board board) => null;

        #endregion
    }
}