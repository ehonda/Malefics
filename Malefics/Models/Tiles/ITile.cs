using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public interface ITile
    {
        public bool IsTraversable();

        public bool Contains(IPiece piece);

        public void Put(IPiece piece);

        public IPiece Take();

        // TODO: - Unit Tests for all subclasses
        //       - Should it return nullable?
        public IPiece? Peek();

        public bool IsOccupied();

        public bool IsValidCaptureTargetFor(IPiece piece);
    }
}
