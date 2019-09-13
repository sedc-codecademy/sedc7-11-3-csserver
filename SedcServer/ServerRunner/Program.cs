using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerRunner
{
    class Program
    {
        static void Main(string[] args)
        {

            byte[] buffer = new byte[10240];
            IPAddress localhost = IPAddress.Parse("127.0.0.1");

            TcpListener listener = new TcpListener(localhost, 13000);
            listener.Start();


            Console.WriteLine("Started listening");

            StringBuilder data = new StringBuilder();
            while(true)
            {
                Console.WriteLine("Before client");
                using (var client = listener.AcceptTcpClient())
                {
                    Console.WriteLine("After client");
                    using (var clientSocket = client.Client)
                    {

                        int receivedCount = clientSocket.Receive(buffer);
                        var readString = Encoding.ASCII.GetString(buffer, 0, receivedCount);

                        Console.WriteLine(readString);

                        var message = @"HTTP/1.1 200 OK
Server: Sedc Demo Server

Hello from SEDC";
                        var messageBytes = Encoding.ASCII.GetBytes(message);

                        clientSocket.Send(messageBytes);

                        //stream.Write(messageBytes, 0, messageBytes.Length);
                        //stream.Flush();
                        //stream.Close();
                    }
                }
            }
        }
    }
}
