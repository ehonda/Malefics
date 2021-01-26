using Malefics.Enums;
using Malefics.Exceptions;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class House : ITile
    {
        public PlayerColor PlayerColor { get; }
        private uint _pawns;

        public House(PlayerColor playerColor, uint pawns)
            => (PlayerColor, _pawns) = (playerColor, pawns);

        #region Implementations of ITile

        /// <inheritdoc />
        public bool Contains(Piece piece)
            => _pawns > 0
               && piece is Pawn pawn
               && pawn == new Pawn(PlayerColor);

        /// <inheritdoc />
        public bool IsGeometricallyTraversable() => true;

        /// <inheritdoc />
        public void Put(Piece piece)
        {
            if (piece is Pawn pawn)
            {
                if (pawn == new Pawn(PlayerColor))
                {
                    ++_pawns;
                    return;
                }
            }

            throw new InvalidTileOperationException(
                $"Can't put {piece} on a house of playerColor {PlayerColor}");
        }

        /// <inheritdoc />
        public Piece Take()
        {
            if (_pawns == 0)
                throw new InvalidTileOperationException(
                    "Can't remove a piece from an empty house.");

            --_pawns;
            return new Pawn(PlayerColor);
        }

        /// <inheritdoc />
        public Piece? Peek() => _pawns > 0 ? new Pawn(PlayerColor) : null;

        /// <inheritdoc />
        public bool IsTraversable() => false;

        /// <inheritdoc />
        public bool IsOccupied() => _pawns != 0;

        /// <inheritdoc />
        public bool IsValidCaptureTargetFor(Piece piece) => false;

        #endregion
    }
}