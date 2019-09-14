namespace RegexDemo
{
    internal class PointData
    {
        public Point Position { get; set; }
        public Point Velocity { get; set; }

        public override string ToString() => $"Position: {Position}, Velocity: {Velocity}";
    }
}