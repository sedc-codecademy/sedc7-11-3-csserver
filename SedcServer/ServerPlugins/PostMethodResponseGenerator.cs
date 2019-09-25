using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;

namespace ServerPlugins
{
    public class PostMethodResponseGenerator : IResponseGenerator
    {
        public int Count { get; }

        public async Task<Response> Generate(Request request, ILogger logger)
        {
            var body = new StringBuilder($@"POST REQUEST INTERCEPTED
Path: { request.Path}
Query: { request.Query}
Body: { request.Body}");

            return new Response
            {
                Body = body.ToString()
            };
        }

        public bool IsInterested(Request request, ILogger logger)
        {
            return request.Method == Method.Post;
        }
    }
}
