using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public interface ITile
    {
        public bool IsTraversable();

        public bool Contains(Piece piece);

        public void Put(Piece piece);

        public Piece Take();

        // TODO: - Unit Tests for all subclasses
        //       - Should it return nullable?
        public Piece? Peek();

        public bool IsOccupied();

        public bool IsValidCaptureTargetFor(Piece piece);
    }
}
