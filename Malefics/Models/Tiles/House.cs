using Malefics.Enums;
using Malefics.Exceptions;
using Malefics.Models.Pieces;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Malefics.Models.Tiles
{
    public class House : Renderable, ITile
    {
        public PlayerColor PlayerColor { get; }
        private uint _pawns;

        public House(PlayerColor playerColor, uint pawns)
            => (PlayerColor, _pawns) = (playerColor, pawns);

        #region Implementations of ITile

        /// <inheritdoc />
        public bool Contains(Piece piece)
            => _pawns > 0
               && piece is Pawn pawn
               && pawn == new Pawn(PlayerColor);

        /// <inheritdoc />
        public bool IsGeometricallyTraversable() => true;

        /// <inheritdoc />
        public void Put(Piece piece)
        {
            if (piece is Pawn pawn)
            {
                if (pawn == new Pawn(PlayerColor))
                {
                    ++_pawns;
                    return;
                }
            }

            throw new InvalidTileOperationException(
                $"Can't put {piece} on a house of playerColor {PlayerColor}");
        }

        /// <inheritdoc />
        public Piece Take()
        {
            if (_pawns == 0)
                throw new InvalidTileOperationException(
                    "Can't remove a piece from an empty house.");

            --_pawns;
            return new Pawn(PlayerColor);
        }

        /// <inheritdoc />
        public Piece? Peek() => _pawns > 0 ? new Pawn(PlayerColor) : null;

        /// <inheritdoc />
        public bool IsTraversable() => false;

        /// <inheritdoc />
        public bool IsOccupied() => _pawns != 0;

        /// <inheritdoc />
        public bool IsValidCaptureTargetFor(Piece piece) => false;

        #endregion

        #region Implementation of ICloneable

        /// <inheritdoc />
        public object Clone() => new House(PlayerColor, _pawns);

        #endregion

        #region Overrides of Renderable

        /// <inheritdoc />
        protected override IEnumerable<Segment> Render(RenderContext context, int maxWidth)
        {
            // TODO: Implement more cleanly
            var houseMarkup = PlayerColor switch
            {
                PlayerColor.Red => new Markup(Colorize("R")),
                PlayerColor.Green => new(Colorize("G")),
                PlayerColor.Yellow => new(Colorize("Y")),
                PlayerColor.Blue => new(Colorize("B")),
                _ => throw new InvalidOperationException(
                    "Unknown player color cannot be rendered.")
            };

            var pawnMarkup = new Markup(Colorize(_pawns.ToString()));

            return (houseMarkup as IRenderable).Render(context, maxWidth)
                .Concat((pawnMarkup as IRenderable).Render(context, maxWidth));

            string Colorize(string s) =>
                PlayerColor switch
                {
                    PlayerColor.Red => $"[red]{s}[/]",
                    PlayerColor.Green => $"[green]{s}[/]",
                    PlayerColor.Yellow => $"[yellow]{s}[/]",
                    PlayerColor.Blue => $"[blue]{s}[/]",
                    _ => throw new InvalidOperationException(
                        "Unknown player color cannot be rendered.")
                };
        }

        #endregion
    }
}