using System;
using System.Collections.Generic;
using System.Linq;
using Malefics.Enums;
using Malefics.Models.Pieces;

namespace Malefics.Models.Tiles
{
    // TODO: Convert to subclasses Road, Rock, etc. of ITile - Tests for subclasses!
    public abstract class Tile : ITile
    {
        protected IList<IPiece> _occupyingPieces = new List<IPiece>();

        public virtual void Remove(IPiece piece) => _occupyingPieces.Remove(piece);

        public bool IsBarricaded() => _occupyingPieces.Contains(new Barricade());

        public virtual bool IsOccupied() => _occupyingPieces.Any();

        public static ITile Rock() => new Rock();

        public static ITile Road() => new Road();

        public static ITile Barricade() => new Road(new Barricade());

        public static ITile Pawn(Player player) => new Road(new Pawn(player));

        public static Tile House(Player player, uint pawns)
            => new House(player, pawns);

        #region Implementation of ITile

        /// <inheritdoc />
        public abstract bool IsTraversable();

        /// <inheritdoc />
        public abstract void Add(IPiece piece);

        #endregion
    }
}
