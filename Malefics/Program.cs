using Malefics.Enums;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using Spectre.Console;

namespace Malefics
{
    class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Render(new Rock());
            AnsiConsole.Render(new Rock());
            AnsiConsole.Render(new Road(new Pawn(PlayerColor.Green)));
            AnsiConsole.Render(new Road(new Pawn(PlayerColor.Green)));
            AnsiConsole.Render(new Road(new Pawn(PlayerColor.Green)));
            AnsiConsole.Render(new Road(new Barricade()));
            AnsiConsole.Render(new Rock());
            AnsiConsole.Render(new Goal());
        }
    }
}
