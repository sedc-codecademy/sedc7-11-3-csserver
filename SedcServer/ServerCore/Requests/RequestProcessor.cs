using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using ServerInterfaces;

namespace ServerCore.Requests
{
    internal static class RequestProcessor
    {
        public static Request ProcessRequest(Socket socket, ILogger logger)
        {
            byte[] buffer = new byte[10240];
            int receivedCount = socket.Receive(buffer);
            logger.Debug($"Received {receivedCount} bytes from socket");
            var readString = Encoding.ASCII.GetString(buffer, 0, receivedCount);
            logger.Debug(readString);
            if (string.IsNullOrEmpty(readString))
            {
                logger.Info("Detected empty request");
                return Request.EmptyRequest;
            }

            var parser = new RequestParser();
            var result = parser.Parse(readString);
            return result;
        }
    }
}
