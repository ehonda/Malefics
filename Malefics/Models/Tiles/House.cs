using Malefics.Enums;
using Malefics.Exceptions;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class House : ITile
    {
        public Player Player { get; }
        private uint _pawns;

        public House(Player player, uint pawns)
            => (Player, _pawns) = (player, pawns);

        #region Implementations of ITile

        /// <inheritdoc />
        public bool Contains(Piece piece)
            => _pawns > 0
               && piece is Pawn pawn
               && pawn == new Pawn(Player);

        /// <inheritdoc />
        public bool IsGeometricallyTraversable() => true;

        /// <inheritdoc />
        public void Put(Piece piece)
        {
            if (piece is Pawn pawn)
            {
                if (pawn == new Pawn(Player))
                {
                    ++_pawns;
                    return;
                }
            }

            throw new InvalidTileOperationException(
                $"Can't put {piece} on a house of player {Player}");
        }

        /// <inheritdoc />
        public Piece Take()
        {
            if (_pawns == 0)
                throw new InvalidTileOperationException(
                    "Can't remove a piece from an empty house.");

            --_pawns;
            return new Pawn(Player);
        }

        /// <inheritdoc />
        public Piece? Peek() => _pawns > 0 ? new Pawn(Player) : null;

        /// <inheritdoc />
        public bool IsTraversable() => false;

        /// <inheritdoc />
        public bool IsOccupied() => _pawns != 0;

        /// <inheritdoc />
        public bool IsValidCaptureTargetFor(Piece piece) => false;

        #endregion
    }
}