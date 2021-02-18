using Malefics.Parsers.Ascii;
using Sprache;

namespace MaleficsTests.Models
{
    public static class ParseBoard
    {
        public static Malefics.Models.Board FromRows(params string[] rows)
            => Grammar.Board().Parse(string.Join('\n', rows));
    }
}