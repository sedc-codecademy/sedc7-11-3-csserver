using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexDemo
{
    internal class RegexPointParser : IPointParser
    {
        public RegexPointParser()
        {
            Console.WriteLine("Constructing RegexPointParser");
        }

        private static readonly Regex parseRegex = new Regex(@"^position=<\s*(-?\d+),\s*(-?\d+)>\svelocity=<\s*(-?\d+),\s*(-?\d+)>$");

        public PointData Parse(string pointString)
        {
            var match = parseRegex.Match(pointString);
            return new PointData
            {
                Position = new Point
                {
                    X = int.Parse(match.Groups[1].Value),
                    Y = int.Parse(match.Groups[2].Value),
                },
                Velocity = new Point
                {
                    X = int.Parse(match.Groups[3].Value),
                    Y = int.Parse(match.Groups[4].Value),
                }
            };
        }
    }
}
