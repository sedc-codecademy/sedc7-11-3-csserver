using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;

namespace ServerPlugins.SqlServer.CommandResponders
{
    class Error : ICommandResponder
    {
        public async Task<Response> GetResponse()
        {
            return new Response
            {
                ContentType = ContentTypes.PlainText,
                ResponseCode = ResponseCode.BadRequest,
                Type = ResponseType.Text,
                Body = "Error in command statement"
            };
        }
    }
}
