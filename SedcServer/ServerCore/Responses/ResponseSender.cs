using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using ServerInterfaces;

namespace ServerCore.Responses
{
    internal static class ResponseSender
    {
        public static void Send(this Response response, Socket socket, WebServerOptions serverOptions)
        {
            var logger = serverOptions.Logger;
            logger.Debug("Start sending response");

            var message = $@"HTTP/1.1 200 OK
Server: {serverOptions.ServerName}

{response.Body}
";
            var messageBytes = Encoding.ASCII.GetBytes(message);
            logger.Debug($"Sending {messageBytes.Length} bytes to socket");
            socket.Send(messageBytes);
            logger.Debug($"Sent {messageBytes.Length} bytes to socket");
        }
    }
}
