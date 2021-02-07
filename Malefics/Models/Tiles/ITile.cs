using System;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    // TODO: We only implement ICloneable because of problems with the parsers
    //       - Is there a better way to solve those problems?
    public interface ITile : ICloneable
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