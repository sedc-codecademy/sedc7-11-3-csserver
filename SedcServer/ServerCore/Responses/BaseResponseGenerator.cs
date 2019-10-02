using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;

namespace ServerCore.Responses
{
    public enum HeaderOptions
    {
        HideHeaders,
        ShowHeaders
    }

    internal class BaseResponseGenerator : IResponseGenerator
    {
        public HeaderOptions ShowHeaders { get; private set; }
        public int Count { get; private set; } = 0;

        public BaseResponseGenerator(HeaderOptions showHeaders = HeaderOptions.HideHeaders)
        {
            ShowHeaders = showHeaders;
        }

        public async Task<Response> Generate(Request request, ILogger logger)
        {
            Count += 1;
            logger.Debug($"Start generating response #{Count}");

            var body = new StringBuilder($@"HODOR { request.Method}
Path: { request.Path}
Query: { request.Query}
Body: { request.Body}");
            if (ShowHeaders == HeaderOptions.ShowHeaders)
            {
                body.AppendLine($@"Headers: {request.Headers}");
            }

            return new Response
            {
                Body = body.ToString()
            };

        }

        public bool IsInterested(Request request, ILogger logger)
        {
            logger.Info("BaseResponseGenerator determining interest");
            if (ShowHeaders == HeaderOptions.ShowHeaders)
            {
                return request.Query.HasParam("headers") 
                    && request.Query.GetParam("headers").Equals(true.ToString(), StringComparison.InvariantCultureIgnoreCase);
            }
            return true;
        }
    }
}
