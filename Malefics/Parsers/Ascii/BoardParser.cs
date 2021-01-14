using Malefics.Models;
using Sprache;

namespace Malefics.Parsers.Ascii
{
    public class BoardParser
    {
        public Board Parse(string board)
        {
            return Grammar.Board.Parse(board);
        }
    }
}
