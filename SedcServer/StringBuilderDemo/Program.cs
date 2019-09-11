using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StringBuilderDemo
{
    class Program
    {
        private static List<string> numbers = new List<string> { "one", "two", "three", "four"};


        static void Main(string[] args)
        {
            var data = Enumerable.Repeat("abcdefg", 80000);
            Console.WriteLine(data.Count());

            // with string addition
            Console.WriteLine("--- String Addition ---");
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = string.Empty;
            foreach (var word in data)
            {
                result += word;
            }
            stopwatch.Stop();
            Console.WriteLine(result.Length);
            Console.WriteLine("Duration: {0} ms",stopwatch.ElapsedMilliseconds);

            Console.WriteLine("--- String Builder ---");
            stopwatch = Stopwatch.StartNew();
            var result2 = new StringBuilder();
            foreach (var word in data)
            {
                result2.Append(word);
            }
            stopwatch.Stop();
            var realResult = result2.ToString();
            Console.WriteLine(realResult.Length);
            Console.WriteLine("Duration: {0} ms", stopwatch.ElapsedMilliseconds);
        }

        private static IEnumerable<string> SquareStrings(IEnumerable<string> input)
        {
            var result = from a in input
                         from b in input
                         select a + b;
            return result;
        }
    }
}
