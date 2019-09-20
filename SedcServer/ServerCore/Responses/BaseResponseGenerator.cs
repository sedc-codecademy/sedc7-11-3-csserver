using System;
using System.Collections.Generic;
using System.Text;
using ServerInterfaces;

namespace ServerCore.Responses
{
    public class BaseResponseGenerator : IResponseGenerator
    {
        public Response Generate(Request request, ILogger logger)
        {
            logger.Debug("Start generating response");
            return new Response
            {
                Body = $@"HODOR { request.Method}
Path: { request.Path}
Query: { request.Query}
Body: { request.Body}"
            };

        }
    }
}
