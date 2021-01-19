using Malefics.Exceptions;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class Road : ITile
    {
        private IPiece? _occupyingPiece;

        public Road() { }

        public Road(IPiece occupyingPiece)
            => _occupyingPiece = occupyingPiece;

        #region Implementations of ITile

        // TODO: Is there a less awkward way to get the value comparison semantics we want?
        /// <inheritdoc />
        public bool Contains(IPiece piece)
            => _occupyingPiece switch
            {
                Barricade => piece switch
                {
                    Barricade => true,
                    _ => false
                },

                Pawn p => piece switch
                {
                    Pawn q => p == q,
                    _ => false
                },

                _ => false
            };

        /// <inheritdoc />
        public void Put(IPiece piece)
        {
            if (_occupyingPiece is not null)
                throw new InvalidTileOperationException(
                    "Can't add a piece to an occupied road tile.");

            _occupyingPiece = piece;
        }

        /// <inheritdoc />
        public IPiece Take()
            => _occupyingPiece
               ?? throw new InvalidTileOperationException(
                   "Can't take a piece from an empty road tile");

        /// <inheritdoc />
        public IPiece? Peek() => _occupyingPiece;

        /// <inheritdoc />
        public bool IsTraversable()
            => _occupyingPiece is not Pieces.Barricade;

        /// <inheritdoc />
        public bool IsOccupied()
            => _occupyingPiece is not null;

        /// <inheritdoc />
        public bool IsValidCaptureTargetFor(IPiece piece)
            => piece is Pawn
               && _occupyingPiece is not null
               && _occupyingPiece != piece;

        #endregion
    }
}