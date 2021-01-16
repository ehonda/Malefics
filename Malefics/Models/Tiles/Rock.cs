using Malefics.Exceptions;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class Rock : ITile
    {
        #region Overrides of Tile

        /// <inheritdoc />
        public void Put(IPiece piece)
            => throw new InvalidTileOperationException("Can't add piece to a rock tile.");

        /// <inheritdoc />
        public IPiece Take()
            => throw new InvalidTileOperationException(
                "Can't take a piece from a rock tile.");

        /// <inheritdoc />
        public bool IsTraversable() => false;

        /// <inheritdoc />
        public bool IsOccupied() => false;

        #endregion
    }
}