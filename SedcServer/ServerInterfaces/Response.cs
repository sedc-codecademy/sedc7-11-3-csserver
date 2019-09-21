using System;
using System.Collections.Generic;
using System.Text;

namespace ServerInterfaces
{
    public class Response
    {
        public string Body { get; set; }
        public byte[] Bytes { get; set; }
        public ResponseType Type { get; set; }

        public static Response EmptyResponse = new Response
        {
            Body = string.Empty,
            Bytes = new byte[0],
            Type = ResponseType.Text
        };

    }
}
