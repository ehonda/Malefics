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

        #region Overrides of Tile

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
        public bool IsTraversable()
            => _occupyingPiece is not Pieces.Barricade;

        /// <inheritdoc />
        public bool IsOccupied()
            => _occupyingPiece is not null;

        #endregion
    }
}