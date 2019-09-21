using System;
using System.Collections.Generic;
using System.Text;

namespace ServerInterfaces
{
    public interface IResponseGenerator
    {
        int Count { get; }

        Response Generate(Request request, ILogger logger);
    }
}
