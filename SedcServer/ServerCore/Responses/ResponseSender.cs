﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var messageBytes = new byte[0];

            if (response.Type == ResponseType.Text)
            {
                var message = $@"HTTP/1.1 200 OK
Server: {serverOptions.ServerName}

{response.Body}
";
                messageBytes = Encoding.ASCII.GetBytes(message);
            }

            if (response.Type == ResponseType.Binary)
            {
                var message = $@"HTTP/1.1 200 OK
Server: {serverOptions.ServerName}
Content-Type: image/jpeg

";
                messageBytes = Encoding.ASCII.GetBytes(message);
                messageBytes = messageBytes.Concat(response.Bytes).ToArray();
            }
            logger.Debug($"Sending {messageBytes.Length} bytes to socket");
            socket.Send(messageBytes);
            logger.Debug($"Sent {messageBytes.Length} bytes to socket");
        }
    }
}
