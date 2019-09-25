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
        public string ContentType { get; set; }
        public ResponseCode ResponseCode { get; set; }

        public Response()
        {
            ResponseCode = ResponseCode.Ok;
            ContentType = ContentTypes.Anything;
        }

        public static Response EmptyResponse = new Response
        {
            Body = string.Empty,
            Bytes = new byte[0],
            ContentType = ContentTypes.Anything,
            Type = ResponseType.Text,
            ResponseCode = ResponseCode.Ok
        };

        //public static Response NotFound = new Response
        //{
        //    ResponseCode = ResponseCode.NotFound
        //};

    }

    public class NotFoundResponse : Response
    {
        public NotFoundResponse()
        {
            Body = string.Empty;
            Bytes = new byte[0];
            ContentType = string.Empty;
            Type = ResponseType.Text;
            ResponseCode = ResponseCode.NotFound;
        }
    }
}
