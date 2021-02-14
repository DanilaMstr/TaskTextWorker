using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TaskTextWorker
{
    class ConsoleTextReader : ITextReader
    {
        public List<string> Read()
        {
            string readedtest = Console.ReadLine();
            List<string> res = 
                readedtest
                .Split(' ')
                .ToList();
            return res;
        }
        public override string ToString() => "Console";
    }
}
