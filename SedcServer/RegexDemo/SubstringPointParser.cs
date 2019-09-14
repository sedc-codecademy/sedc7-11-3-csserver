using System;
using System.Collections.Generic;
using System.Text;

namespace RegexDemo
{
    internal class SubstringPointParser : IPointParser
    {
        public SubstringPointParser()
        {
            Console.WriteLine("Constructing SubstringPointParser");
        }


        public PointData Parse(string pointString)
        {
            var line = pointString;
            line = line.Substring(10);
            var posx = int.Parse(line.Substring(0, 6));
            line = line.Substring(8);
            var posy = int.Parse(line.Substring(0, 6));
            line = line.Substring(18);
            var velx = int.Parse(line.Substring(0, 2));
            line = line.Substring(4);
            var vely = int.Parse(line.Substring(0, 2));

            return new PointData
            {
                Position = new Point { X = posx, Y = posy },
                Velocity = new Point { X = velx, Y = vely }
            };
        }
    }
}
