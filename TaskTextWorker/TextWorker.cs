using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

namespace TaskTextWorker
{
    class TextWorker
    {
        private ConcurrentDictionary<string, ushort> pairs = new ConcurrentDictionary<string, ushort>();
        //private Dictionary<string, ushort> pairs = new Dictionary<string, ushort>();
        private int countCharacter;
        //private List<string> text;
        private ConcurrentBag<string> text;
        private bool isPressKey = false;

        public string GetStatistics(
            List<string> text, 
            int countTop = 10,
            int countCharacter=3)
        {
            pairs.Clear();
            //this.text = text;
            this.text = new ConcurrentBag<string>(text);
            this.countCharacter = countCharacter;

            Start();

            return gerResultOnString(countTop);
        }

        private void Start()
        {
            text.AsParallel()
                .Where(str => str.Length >= countCharacter &&
                                    Regex.IsMatch(str, "^[A-ZА-Я]+$")
                                    && !isPressKey)
                .ForAll(str =>
                {
                    //CheckPressKeyOnKeyBord();
                    checkNode(str);
                });
        }

        async private void CheckPressKeyOnKeyBord()
        {
            await Task.Run(() => 
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter)
                {
                    isPressKey = true;
                }
            });
        }

        private string gerResultOnString(int countTop)
        {
            string res = string.Empty;

            foreach (var i in pairs.OrderByDescending(p => p.Value).Take(countTop))
                res += i.Key + ", ";

            return res;
        }

        private void checkNode(string node)
        {
            int size = node.Length;
            int countSkip = 0;
            while (countSkip + countCharacter <= size)
            {
                string characters = getConcatStrings(node.Skip(countSkip).Take(countCharacter).ToList());
                countSkip++;
                if (pairs.ContainsKey(characters))
                    pairs[characters]++;
                else
                    pairs.TryAdd(characters, 1);
            }
        }
        private string getConcatStrings(List<char> strings)
        {
            string res = string.Empty;
            foreach (var str in strings)
                res += str;
            return res;
        }
    }
}
