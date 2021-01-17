using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public interface ITile
    {
        public bool IsTraversable();

        public bool Contains(IPiece piece);

        public void Put(IPiece piece);

        public IPiece Take();

        public bool IsOccupied();

        public bool IsValidCaptureTargetFor(IPiece piece);
    }
}
