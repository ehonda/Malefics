using Malefics.Extensions;
using Malefics.Models;
using System;
using System.Linq;

namespace Malefics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{Path.AxisParallelSegments(new Position(0, 0)).Any()}");
        }
    }
}
