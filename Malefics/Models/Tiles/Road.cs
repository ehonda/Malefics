using System.Collections.Generic;
using Malefics.Exceptions;
using Malefics.Models.Pieces;
using Spectre.Console.Rendering;

namespace Malefics.Models.Tiles
{
    public class Road : Renderable, ITile
    {
        private Piece? _occupyingPiece;

        public Road() { }

        public Road(Piece occupyingPiece)
            => _occupyingPiece = occupyingPiece;

        #region Implementations of ITile

        // TODO: Is there a less awkward way to get the value comparison semantics we want?
        /// <inheritdoc />
        public bool Contains(Piece piece) => _occupyingPiece == piece;

        /// <inheritdoc />
        public bool IsGeometricallyTraversable() => true;

        /// <inheritdoc />
        public void Put(Piece piece)
        {
            if (_occupyingPiece is not null)
                throw new InvalidTileOperationException(
                    "Can't add a piece to an occupied road tile.");

            _occupyingPiece = piece;
        }

        /// <inheritdoc />
        public Piece Take()
        {
            if(_occupyingPiece is null)
                throw new InvalidTileOperationException(
                    "Can't take a piece from an empty road tile");

            var piece = _occupyingPiece;
            _occupyingPiece = null;
            return piece;
        }

        /// <inheritdoc />
        public Piece? Peek() => _occupyingPiece;

        /// <inheritdoc />
        public bool IsTraversable()
            => _occupyingPiece is not Pieces.Barricade;

        /// <inheritdoc />
        public bool IsOccupied()
            => _occupyingPiece is not null;

        /// <inheritdoc />
        public bool IsValidCaptureTargetFor(Piece piece)
            => piece is Pawn
               && _occupyingPiece is not null
               && _occupyingPiece != piece;

        #endregion

        #region Implementation of ICloneable

        /// <inheritdoc />
        public object Clone()
            => _occupyingPiece is not null
                ? new(_occupyingPiece with { })
                : new Road();

        #endregion

        #region Overrides of Renderable

        /// <inheritdoc />
        protected override IEnumerable<Segment> Render(RenderContext context, int maxWidth)
        {
            yield break;
        }

        #endregion
    }
}