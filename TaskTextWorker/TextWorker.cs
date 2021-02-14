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
        private int countCharacter;
        private ConcurrentBag<string> text;

        public string GetStatistics(
            List<string> text, 
            int countTop = 10,
            int countCharacter=3)
        {
            if (text.Count == 0)
                return "Empty";

            pairs.Clear();
            this.text = new ConcurrentBag<string>(text);
            this.countCharacter = countCharacter;

            Start();

            return gerResultOnString(countTop);
        }

        private void Start()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey(true);
                    cts.Cancel();
            });

            try
            {
                Parallel.ForEach(text
                    .Where(str => str.Length >= countCharacter && 
                        Regex.IsMatch(str, "^[A-ZА-Я]+$")), 
                                 po, 
                                 (str) => 
                                 {
                                     checkNode(str);
                                     po.CancellationToken.ThrowIfCancellationRequested();
                                 });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Operation was interrupted");
            }
            finally
            {
                cts.Dispose();
            }
        }

        private string gerResultOnString(int countTop)
        {
            string res = string.Empty;

            foreach (var i in pairs.OrderByDescending(p => p.Value).Take(countTop))
                res += i.Key + ", ";
            res = res.Remove(res.Length - 1);
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
