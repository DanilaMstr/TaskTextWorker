using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskTextWorker
{
    class FileTextReader : ITextReader
    {
        public string filePath=string.Empty;
        public FileTextReader(string filePath)
        {
            this.filePath = filePath;
        }
        public List<string> Read()
        {
            //string FilePath = Console.ReadLine();
            List<string> res = new List<string>();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found");
                return res;
            }
            else
            {
                Console.WriteLine($"Reading file {filePath}");
                string readText = File.ReadAllText(filePath).ToUpper();
                readText = readText.Replace("\r\n"," ");
                res = readText.Split(' ').ToList();
                Console.WriteLine($"File read successfully");
            }
            return res;
        }
        public override string ToString() => "File";
    }
}
