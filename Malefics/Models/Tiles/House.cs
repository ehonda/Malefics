using System;
using Malefics.Enums;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class House : Tile, ITile
    {
        private readonly Player _player;
        private uint _pawns;

        public House(Player player, uint pawns)
            => (_player, _pawns) = (player, pawns);

        #region Overrides of Tile

        /// <inheritdoc />
        public override void Add(IPiece piece) => ++_pawns;

        /// <inheritdoc />
        public override IPiece Take()
        {
            if (_pawns == 0)
                throw new InvalidOperationException(
                    "Can't remove a piece from an empty house.");

            --_pawns;
            return new Pawn(_player);
        }

        /// <inheritdoc />
        public override bool IsTraversable() => false;

        /// <inheritdoc />
        public override bool IsOccupied() => _pawns != 0;

        #endregion
    }
}