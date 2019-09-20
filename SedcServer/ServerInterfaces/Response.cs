using System;
using System.Collections.Generic;
using System.Text;

namespace ServerInterfaces
{
    public class Response
    {
        public string Body { get; set; }

        public static Response EmptyResponse = new Response
        {
            Body = string.Empty
        };

    }
}
