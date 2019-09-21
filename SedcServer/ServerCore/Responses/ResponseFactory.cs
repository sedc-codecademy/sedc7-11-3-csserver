using System;
using ServerInterfaces;

namespace ServerCore.Responses
{
    internal static class ResponseFactory
    {
        private static readonly ImageResponseGenerator imagerg = new ImageResponseGenerator();

        internal static IResponseGenerator GetGenerator(Request request, ILogger logger)
        {
            if (request.Query.HasParam("headers") && request.Query.GetParam("headers").Equals(true.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return new BaseResponseGenerator(HeaderOptions.ShowHeaders);
            }

            if (request.Path.EndsWith(".jpg"))
            {
                return imagerg;
            }
            return new BaseResponseGenerator();
        }
    }
}