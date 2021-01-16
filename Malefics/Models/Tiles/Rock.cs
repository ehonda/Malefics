using System;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class Rock : Tile, ITile
    {
        #region Overrides of Tile

        /// <inheritdoc />
        public override void Put(IPiece piece)
            => throw new InvalidOperationException("Can't add piece to a rock tile.");

        /// <inheritdoc />
        public override bool IsTraversable() => false;

        #endregion
    }
}