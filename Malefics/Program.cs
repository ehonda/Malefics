using Malefics.Enums;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using Malefics.Parsers.Ascii;
using Spectre.Console;
using Sprache;

namespace Malefics
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = Grammar.Board().Parse(
@"r...x..b.
.       .
.........
 R2    B2");

            AnsiConsole.Render(new Panel(board));
            //AnsiConsole.Render(board);
            //AnsiConsole.Render(new Rock());
            //AnsiConsole.Render(new Rock());
            //AnsiConsole.Render(new Road(new Pawn(PlayerColor.Green)));
            //AnsiConsole.Render(new Road(new Pawn(PlayerColor.Green)));
            //AnsiConsole.Render(new Road(new Pawn(PlayerColor.Green)));
            //AnsiConsole.Render(new Road(new Barricade()));
            //AnsiConsole.Render(new Rock());
            //AnsiConsole.Render(new Goal());
            //AnsiConsole.Render(new House(PlayerColor.Red, 0));
            //AnsiConsole.Render(new House(PlayerColor.Blue, 1));
            //AnsiConsole.Render(new House(PlayerColor.Green, 4));
        }
    }
}
