using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class House : Tile, ITile
    {
        #region Overrides of Tile

        /// <inheritdoc />
        public override void Add(IPiece piece)
            => _occupyingPieces.Add(piece);

        /// <inheritdoc />
        public override bool IsTraversable() => false;

        #endregion
    }
}