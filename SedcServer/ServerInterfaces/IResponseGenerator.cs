using System;
using System.Collections.Generic;
using System.Text;

namespace ServerInterfaces
{
    public interface IResponseGenerator
    {
        Response Generate(Request request, ILogger logger);
    }
}
