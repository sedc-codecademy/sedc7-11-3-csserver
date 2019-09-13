using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ServerCore;
using ServerInterfaces;

namespace ServerRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new WebServer();
            server.Start();
        }
    }
}
