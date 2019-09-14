using System;
using System.Collections.Generic;
using System.Text;

namespace RegexDemo
{
    internal interface IPointParser
    {
        PointData Parse(string pointString);
    }
}
