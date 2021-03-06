﻿using Malefics.Exceptions;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class Rock : ITile
    {
        #region Implementations of ITile

        /// <inheritdoc />
        public bool Contains(Piece piece) => false;

        /// <inheritdoc />
        public bool IsGeometricallyTraversable() => false;

        /// <inheritdoc />
        public void Put(Piece piece)
            => throw new InvalidTileOperationException("Can't add piece to a rock tile.");

        /// <inheritdoc />
        public Piece Take()
            => throw new InvalidTileOperationException(
                "Can't take a piece from a rock tile.");

        /// <inheritdoc />
        public Piece? Peek() => null;

        /// <inheritdoc />
        public bool IsTraversable() => false;

        /// <inheritdoc />
        public bool IsOccupied() => false;

        /// <inheritdoc />
        public bool IsValidCaptureTargetFor(Piece piece) => false;

        #endregion

        #region Implementation of ICloneable

        /// <inheritdoc />
        public object Clone() => new Rock();

        #endregion
    }
}