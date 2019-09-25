using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServerInterfaces
{
    public interface IResponsePostProcessor
    {
        bool IsInterested(Response response, ILogger logger);

        Task<Response> Process(Response response, ILogger logger);
    }

}
