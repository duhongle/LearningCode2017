using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DotNetBasicConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();
            sw.Reset();
            var ss = new SortedSet<int>(Enumerable.Repeat(42, 400_000));
            Console.WriteLine(sw.Elapsed);

            Console.ReadLine();
        }
    }
}
