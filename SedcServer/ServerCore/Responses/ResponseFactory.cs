using System;
using ServerInterfaces;

namespace ServerCore.Responses
{
    internal static class ResponseFactory
    {
        internal static IResponseGenerator GetGenerator(Request request, ILogger logger)
        {
            if (request.Path.EndsWith(".jpg"))
            {
                return new ImageResponseGenerator();
            }
            return new BaseResponseGenerator();
        }
    }
}