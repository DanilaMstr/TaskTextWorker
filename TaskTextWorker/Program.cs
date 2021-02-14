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
            while(true)
            {
                Console.Write("Write filepath : ");
                string filePath = Console.ReadLine();
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                ITextReader reader = new FileTextReader(filePath);
                ITextWriter writer = new ConsoleTextWriter();
                TextWorker textWorker = new TextWorker();
                writer.Write(textWorker.GetStatistics(reader.Read()));
                stopWatch.Stop();
                Console.WriteLine($"RunTime: {stopWatch.ElapsedMilliseconds} milliseconds");

                Console.WriteLine("Write any key to repeat or write c to exist");
                string isContinue = Console.ReadLine();
                if (isContinue == "c")
                    break;
            }
        }
    }
}
