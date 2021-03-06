﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace DotNetBasicConsoleApp
{
    class Program
    {
        static void Main(string[] args) => StartFunc();

        static void StartFunc()
        {
            var service = InvokeService.Proxy<IUserService>();
            Console.WriteLine($"当前用户名：{service.GetCurrentsUserName()}");

            var sw = Stopwatch.StartNew();
            var ss = new SortedSet<int>(Enumerable.Repeat(42, 400_000));
            Console.WriteLine(sw.Elapsed);

            Console.ReadLine();
        }

        static void WriterFunc()
        {
            using (var writer = new BinaryWriter(Console.OpenStandardOutput()))
            {
                byte p = 0;
                do
                {
                    writer.Write(p);
                    Thread.Sleep(50);
                    p++;
                }
                while (p <= 100);
                writer.Close();
            }
        }
    }
}
