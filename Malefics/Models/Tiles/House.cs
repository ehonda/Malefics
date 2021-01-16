using Malefics.Enums;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class House : Tile, ITile
    {
        private readonly Player _player;

        public House(Player player)
            => _player = player;

        #region Overrides of Tile

        /// <inheritdoc />
        public override void Add(IPiece piece)
            => _occupyingPieces.Add(piece);

        /// <inheritdoc />
        public override bool IsTraversable() => false;

        #endregion
    }
}