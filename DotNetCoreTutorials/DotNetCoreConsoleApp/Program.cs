using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotNetCoreClassLibrary;

namespace DotNetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(BaseEntity.Get(512));

            var sw = Stopwatch.StartNew();
            while (true)
            {
                sw.Reset();
                sw.Start();

                var ss = new SortedSet<int>(Enumerable.Repeat(42, 400_000));

                var q = new Queue<int>();
                for (int i = 0; i < 200_000_000; i++)
                {
                    q.Enqueue(i);
                    q.Dequeue();
                }

                var result = 0;
                var s = new SortedSet<int>();
                for (int i = 0; i < 100_000; i++)
                {
                    s.Add(i);
                }
                for (int i = 0; i < 100_000_000; i++)
                {
                    result = s.Min;
                }

                var list = new List<int>();
                for (int i = 0; i < 100_100_100; i++)
                {
                    list.Add(i);
                    list.RemoveAt(0);
                }

                Console.WriteLine(sw.Elapsed);
            }
        }


    }
}
