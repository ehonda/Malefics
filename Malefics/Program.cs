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
            AnsiConsole.Render(new Rock());
            AnsiConsole.Render(new Rock());
            AnsiConsole.Render(new Goal());
        }
    }
}
