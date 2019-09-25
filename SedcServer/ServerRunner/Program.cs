﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PngResponseGeneratorLib;
using ServerCore;
using ServerInterfaces;
using ServerPlugins;

namespace ServerRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new WebServer();
            server
                .UseResponseGenerator<PngResponseGenerator>()
                .UseResponseGenerator<PostMethodResponseGenerator>()
                .UseResponseGenerator(new StaticResponseGenerator(@"C:\Users\Weko\OneDrive\Memes"))
                .UseResponseGenerator(new StaticResponseGenerator(@"C:\Source\SEDC\sedc7-04-ajs\g2\Workshop\Game\Code"))
                .UseResponsePostProcessor<NotFoundPostProcessor>();
            

            var result = server.Run();
            result.Wait();
        }
    }
}
