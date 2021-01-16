﻿using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    public interface ITile
    {
        public bool IsTraversable();

        public void Add(IPiece piece);

        public IPiece Take();

        public bool IsOccupied();
    }
}
