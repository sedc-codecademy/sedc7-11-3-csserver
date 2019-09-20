using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ServerCore.Logger;
using ServerCore.Requests;
using ServerCore.Responses;
using ServerInterfaces;

namespace ServerCore
{
    public class WebServer
    {
        private readonly ILogger logger;

        public int Port { get; private set; }
        public string ServerName { get; private set; }

        private WebServerOptions serverOptions;

        public WebServer(WebServerOptions options = null)
        {
            serverOptions = WebServerOptions.Default.Merge(options);
            logger = serverOptions.Logger;
            Port = serverOptions.Port;
            ServerName = serverOptions.ServerName;
        }

        public void Start()
        {
            try
            {
                
                IPAddress localhost = IPAddress.Parse("127.0.0.1");

                TcpListener listener = new TcpListener(localhost, Port);
                listener.Start();

                logger.Info($"Server {ServerName} Started listening on port {Port}");

                while (true)
                {
                    using (var client = listener.AcceptTcpClient())
                    {
                        logger.Info("Accepted a client");
                        using (var clientSocket = client.Client)
                        {
                            // https://steve-yegge.blogspot.com/2006/03/execution-in-kingdom-of-nouns.html
                            var request = RequestProcessor.ProcessRequest(clientSocket, logger);

                            var generator = ResponseFactory.GetGenerator(request, logger);
                            var response = generator.Generate(request, logger);

                            response.Send(clientSocket, serverOptions);
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
