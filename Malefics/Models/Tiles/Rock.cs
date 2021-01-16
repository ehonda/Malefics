using Malefics.Exceptions;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class Rock : Tile, ITile
    {
        #region Overrides of Tile

        /// <inheritdoc />
        public override void Put(IPiece piece)
            => throw new InvalidTileOperationException("Can't add piece to a rock tile.");

        /// <inheritdoc />
        public override IPiece Take()
            => throw new InvalidTileOperationException(
                "Can't take a piece from a rock tile.");

        /// <inheritdoc />
        public override bool IsTraversable() => false;

        /// <inheritdoc />
        public override bool IsOccupied() => false;

        #endregion
    }
}