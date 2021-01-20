using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public class Goal : ITile
    {
        #region Implementation of ITile

        /// <inheritdoc />
        public bool IsTraversable()
        {
            return false;
        }

        /// <inheritdoc />
        public bool Contains(Piece piece)
        {
            return false;
        }

        /// <inheritdoc />
        public void Put(Piece piece)
        {
        }

        /// <inheritdoc />
        public Piece Take()
        {
            return null;
        }

        /// <inheritdoc />
        public Piece? Peek()
        {
            return null;
        }

        /// <inheritdoc />
        public bool IsOccupied()
        {
            return false;
        }

        /// <inheritdoc />
        public bool IsValidCaptureTargetFor(Piece piece)
        {
            return false;
        }

        #endregion
    }
}
