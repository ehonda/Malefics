using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public interface ITile
    {
        public bool Contains(Piece piece);

        public bool IsGeometricallyTraversable();

        public bool IsOccupied();

        public bool IsTraversable();

        public bool IsValidCaptureTargetFor(Piece piece);

        // TODO: - Unit Tests for all subclasses
        //       - Should it return nullable?
        public Piece? Peek();

        public void Put(Piece piece);

        public Piece Take();
    }
}