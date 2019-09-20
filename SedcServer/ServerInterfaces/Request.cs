using System;
using System.Collections.Generic;
using System.Text;

namespace ServerInterfaces
{
    /// <summary>
    /// Encapsulates the data for a HTTP/1.1 request
    /// </summary>
    public class Request
    {
        public Method Method { get; set; }
        public string Path { get; set; }
        public QueryParamCollection Query { get; set; }
        public HeaderCollection Headers { get; set; }
        public string Body { get; set; }

        public static Request EmptyRequest = new Request
        {
            Method = Method.None,
            Path = string.Empty,
            Query = QueryParamCollection.Empty,
            Headers = HeaderCollection.Empty,
            Body = string.Empty
        };
    }
}
