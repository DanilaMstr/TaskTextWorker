using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTextWorker
{
    class ConsoleTextWriter : ITextWriter
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
        public override string ToString() => "Console";
    }
}
