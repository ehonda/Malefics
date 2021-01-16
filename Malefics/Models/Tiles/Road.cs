﻿using System;
using System.Linq;
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
        public override void Add(IPiece piece)
        {
            if (_occupyingPiece is not null)
                throw new InvalidOperationException(
                    "Can't add a piece to an occupied road tile.");
            
            _occupyingPiece = piece;
        }

        /// <inheritdoc />
        public override bool IsTraversable()
            => _occupyingPiece is not Pieces.Barricade;

        #endregion
    }
}