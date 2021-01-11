using Malefics.Extensions;
using System;
using System.Linq;

namespace Malefics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{Path.AxisParallelSegments().Any()}");
        }
    }
}
