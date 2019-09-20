using System;
using System.Collections.Generic;
using System.Text;
using ServerInterfaces;

namespace ServerCore.Responses
{
    class ImageResponseGenerator : IResponseGenerator
    {
        public Response Generate(Request request, ILogger logger)
        {
            return new Response
            {
                Body = "Pretend this is an image"
            };
        }
    }
}
