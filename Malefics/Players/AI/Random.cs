using System.Collections.Generic;
using Malefics.Enums;
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
        {
            yield break;
        }

        /// <inheritdoc />
        public Position RequestBarricadePlacement(Board board) => null;

        #endregion
    }
}