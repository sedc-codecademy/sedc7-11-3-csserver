using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ServerCore.Logger;
using ServerCore.Requests;
using ServerInterfaces;

namespace ServerCore
{
    public class WebServer
    {
        private readonly ILogger logger;

        public WebServer(ILogger logger = null)
        {
            this.logger = logger ?? new ConsoleLogger();
        }

        public void Start()
        {
            try
            {
                
                IPAddress localhost = IPAddress.Parse("127.0.0.1");

                TcpListener listener = new TcpListener(localhost, 13000);
                listener.Start();

                logger.Info("Started listening");

                while (true)
                {
                    using (var client = listener.AcceptTcpClient())
                    {
                        logger.Info("Accepted a client");
                        using (var clientSocket = client.Client)
                        {
                            var request = RequestProcessor.ProcessRequest(clientSocket, logger);

                            var message = $@"HTTP/1.1 200 OK
Server: Sedc Demo Server

HODOR {request.Method}
Path: {request.Path}
Query: {request.Query}
Body: {request.Body}
Headers: {request.Headers}
";
                            var messageBytes = Encoding.ASCII.GetBytes(message);
                            logger.Debug($"Sending {messageBytes.Length} bytes to socket");
                            clientSocket.Send(messageBytes);
                            logger.Debug($"Sent {messageBytes.Length} bytes to socket");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured in webserver", ex);
                throw;
            }

        }

    }
}
