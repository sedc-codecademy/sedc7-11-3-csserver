using System;
using System.Collections.Generic;
using System.Text;
using ServerInterfaces;

namespace ServerCore.Responses
{
    public enum HeaderOptions
    {
        HideHeaders,
        ShowHeaders
    }

    public class BaseResponseGenerator : IResponseGenerator
    {
        public HeaderOptions ShowHeaders { get; private set; }
        public int Count { get; private set; } = 0;

        public BaseResponseGenerator(HeaderOptions showHeaders = HeaderOptions.HideHeaders)
        {
            ShowHeaders = showHeaders;
        }

        public Response Generate(Request request, ILogger logger)
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
    }
}
