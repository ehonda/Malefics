using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class Road : Tile, ITile
    {
        #region Overrides of Tile

        /// <inheritdoc />
        public override void Add(IPiece piece)
            => _occupyingPieces.Add(piece);

        /// <inheritdoc />
        public override bool IsTraversable()
            => !_occupyingPieces.Contains(new Barricade());

        #endregion
    }
}