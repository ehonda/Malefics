using System.Collections.Generic;
using Malefics.Exceptions;
using Malefics.Models.Pieces;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Malefics.Models.Tiles
{
    public class Goal : Renderable, ITile
    {
        // TODO: What to do about the other functions we need to implement?
        //       They aren't really meaningful since if a Pawn is placed on
        //       a Goal, the game ends, should we try and realize "natural"
        //       semantics for them anyway?

        #region Implementation of ITile

        /// <inheritdoc />
        public bool IsTraversable() => true;

        /// <inheritdoc />
        public bool Contains(Piece piece)
        {
            return false;
        }

        /// <inheritdoc />
        public bool IsGeometricallyTraversable() => true;

        /// <inheritdoc />
        public void Put(Piece piece)
        {
        }

        // TODO: Test that this throws
        /// <inheritdoc />
        public Piece Take()
            => throw new InvalidTileOperationException("Can't take from a Goal tile.");

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

        #region Implementation of ICloneable

        /// <inheritdoc />
        public object Clone() => new Goal();

        #endregion

        #region Overrides of Renderable

        /// <inheritdoc />
        protected override IEnumerable<Segment> Render(RenderContext context, int maxWidth)
            => (new Markup("[darkgoldenrod]x[/]") as IRenderable).Render(context, maxWidth);

        #endregion
    }
}
