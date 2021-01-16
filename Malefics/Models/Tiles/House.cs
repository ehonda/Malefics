using Malefics.Enums;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class House : Tile, ITile
    {
        private readonly Player _player;
        private uint _pawns;

        public House(Player player)
            => _player = player;

        #region Overrides of Tile

        /// <inheritdoc />
        public override void Add(IPiece piece) => ++_pawns;

        /// <inheritdoc />
        public override bool IsTraversable() => false;

        /// <inheritdoc />
        public override bool IsOccupied() => _pawns != 0;

        #endregion
    }
}