using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServerInterfaces
{
    public interface IResponseGenerator
    {
        int Count { get; }

        bool IsInterested(Request request, ILogger logger);

        Task<Response> Generate(Request request, ILogger logger);
    }

}


