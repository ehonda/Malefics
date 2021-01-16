using System;
using System.Linq;
using Malefics.Exceptions;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class Road : Tile, ITile
    {
        private IPiece? _occupyingPiece;

        public Road() { }

        public Road(IPiece occupyingPiece)
            => _occupyingPiece = occupyingPiece;

        #region Overrides of Tile

        /// <inheritdoc />
        public override void Put(IPiece piece)
        {
            if (_occupyingPiece is not null)
                throw new InvalidOperationException(
                    "Can't add a piece to an occupied road tile.");
            
            _occupyingPiece = piece;
        }

        /// <inheritdoc />
        public override IPiece Take()
            => _occupyingPiece
               ?? throw new InvalidTileOperationException(
                   "Can't take a piece from an empty road tile");

        /// <inheritdoc />
        public override bool IsTraversable()
            => _occupyingPiece is not Pieces.Barricade;

        #endregion
    }
}