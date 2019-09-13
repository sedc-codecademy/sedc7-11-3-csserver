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

            while (true)
            {
                var client = listener.AcceptTcpClient();
                var clientSocket = client.Client;

                var receivedCount = clientSocket.Receive(buffer);
                var request = Encoding.UTF8.GetString(buffer, 0, receivedCount);

                Console.WriteLine(request);

                StringBuilder sb = new StringBuilder($"HTTP/1.1 200 OK\r\nServer: SEDC Data Web Server\r\n");
                var payload = "HELLO FROM SEDC Server";
                var payloadBytes = Encoding.UTF8.GetBytes(payload);
                sb.AppendLine($"Content-Length: {payloadBytes.Length}");
                sb.AppendLine();
                sb.AppendLine(payload);
                var responseBytes = Encoding.UTF8.GetBytes(sb.ToString());

                clientSocket.Send(responseBytes);
                clientSocket.Close();
                client.Close();
            }
        }
    }
}
