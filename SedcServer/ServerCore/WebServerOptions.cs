using System;
using System.Collections.Generic;
using System.Text;
using ServerCore.Logger;
using ServerInterfaces;

namespace ServerCore
{
    public class WebServerOptions
    {
        public ILogger Logger { get; set; }
        public int Port { get; set; }
        public string ServerName { get; set; }

        internal static WebServerOptions Default = new WebServerOptions
        {
            Logger = new ConsoleLogger(),
            Port = 2019,
            ServerName = "Sedc Demo Server"
        };

        internal WebServerOptions Merge(WebServerOptions options)
        {
            if (options == null)
            {
                return this;
            }

            return new WebServerOptions
            {
                Logger = options.Logger ?? Logger,
                Port = (options.Port != 0) ? options.Port : Port,
                ServerName = !string.IsNullOrEmpty(options.ServerName) ? options.ServerName : ServerName
            };
        }
    }
}
