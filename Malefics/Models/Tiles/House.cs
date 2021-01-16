using Malefics.Enums;
using Malefics.Exceptions;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class House : ITile
    {
        private readonly Player _player;
        private uint _pawns;

        public House(Player player, uint pawns)
            => (_player, _pawns) = (player, pawns);

        #region Implementations of ITile

        /// <inheritdoc />
        public void Put(IPiece piece)
        {
            if (piece is Pawn pawn)
            {
                if (pawn == new Pawn(_player))
                {
                    ++_pawns;
                    return;
                }
            }

            throw new InvalidTileOperationException(
                $"Can't put {piece} on a house of player {_player}");
        }

        /// <inheritdoc />
        public IPiece Take()
        {
            if (_pawns == 0)
                throw new InvalidTileOperationException(
                    "Can't remove a piece from an empty house.");

            --_pawns;
            return new Pawn(_player);
        }

        /// <inheritdoc />
        public bool IsTraversable() => false;

        /// <inheritdoc />
        public bool IsOccupied() => _pawns != 0;

        /// <inheritdoc />
        public bool IsValidCaptureTargetFor(IPiece piece)
        {
            return false;
        }

        #endregion
    }
}