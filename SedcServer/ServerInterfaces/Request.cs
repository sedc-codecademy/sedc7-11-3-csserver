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
        public string Query { get; set; }
        public HeaderCollection Headers { get; set; }
        public string Body { get; set; }
    }
}
