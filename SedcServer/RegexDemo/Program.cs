using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // ugly code
            var algorithm = args[0] ?? "substring";

            // the source is the input for advent-of-code, year 2018, day 10
            var lines = File.ReadAllLines("regex-01.txt");

            IPointParser parser = ParserFactory.GetParser(algorithm);

            var points = lines.Select(line => parser.Parse(line));

            var fastest = points.OrderByDescending(point => point.Velocity.Distance()).First();

            Console.WriteLine($"The fastest point is {fastest}");

            var fastestOnes = points
                .GroupBy(point => point.Velocity.Distance())
                .OrderByDescending(group => group.Key)
                .First();

            Console.WriteLine("Actually it's a tie between:");
            foreach (var point in fastestOnes)
            {
                Console.WriteLine($"{point}");
            }

        }
    }
}
