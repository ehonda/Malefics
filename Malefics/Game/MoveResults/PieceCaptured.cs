using Malefics.Models.Pieces;

namespace Malefics.Game.MoveResults
{
    public record PieceCaptured(Piece Piece) : MoveResult;
}
