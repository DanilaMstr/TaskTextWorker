using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTextWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string filePath = @"C:\Users\danil\source\repos\TaskTextWorker\TaskTextWorker\test.txt";
            stopWatch.Start();
            ITextReader reader = new FileTextReader(filePath);
            ITextWriter writer = new ConsoleTextWriter();
            TextWorker textWorker = new TextWorker();
            writer.Write(textWorker.GetStatistics(reader.Read()));
            stopWatch.Stop();
            Console.WriteLine($"RunTime: {stopWatch.ElapsedMilliseconds} milliseconds");
            Console.ReadLine();
        }
    }
}
