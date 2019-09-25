using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
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
        private ResponseFactory responseFactory;

        public WebServer(WebServerOptions options = null)
        {
            serverOptions = WebServerOptions.Default.Merge(options);
            logger = serverOptions.Logger;
            Port = serverOptions.Port;
            ServerName = serverOptions.ServerName;

            responseFactory = new ResponseFactory();
        }

        public WebServer Use<T>() where T: IResponseGenerator, new()
        {
            var responseGenerator = new T();
            responseFactory.RegisterGenerator(responseGenerator);
            return this;
        }


        public async Task Run()
        {
            try
            {
                
                IPAddress localhost = IPAddress.Parse("127.0.0.1");

                TcpListener listener = new TcpListener(localhost, Port);
                listener.Start();

                logger.Info($"Server {ServerName} Started listening on port {Port}");

                while (true)
                {
                    using (var client = await listener.AcceptTcpClientAsync())
                    {
                        logger.Info("Accepted a client");
                        using (var clientSocket = client.Client)
                        {
                            // we can have an extension here, that changes the raw data
                            var request = RequestProcessor.ProcessRequest(clientSocket, logger);
                            // we can have an extension here, that modifies the parsed request

                            // we can have extensions inside the factory
                            var generator = responseFactory.GetGenerator(request, logger);
                            var response = await generator.Generate(request, logger);
                            // we can have an extension here, that modifies the generated response
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
