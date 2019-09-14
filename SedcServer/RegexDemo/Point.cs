using System;

namespace RegexDemo
{
    internal class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString() => $"{{X: {X}, Y: {Y}}}";

        public int Distance()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }
    }
}