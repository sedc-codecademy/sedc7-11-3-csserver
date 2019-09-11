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

            byte[] bytes = new byte[256];
            IPAddress localhost = IPAddress.Parse("127.0.0.1");

            TcpListener listener = new TcpListener(localhost, 13000);
            listener.Start();


            Console.WriteLine("Started listening");

            StringBuilder data = new StringBuilder();
            while(true)
            {
                Console.WriteLine("Before client");
                var client = listener.AcceptTcpClient();
                Console.WriteLine("After client");
                var stream = client.GetStream();

                var readCount = stream.Read(bytes, 0, bytes.Length);
                while (readCount != 0)
                {
                    var readString = Encoding.ASCII.GetString(bytes, 0, readCount);
                    data.Append(readString);
                    readCount = stream.Read(bytes, 0, bytes.Length);
                }

                Console.WriteLine(data.ToString());

                var message = " Hello from SEDC";
                var messageBytes = Encoding.ASCII.GetBytes(message);
                stream.Write(messageBytes, 0, messageBytes.Length);
                stream.Flush();
                stream.Close();
                client.Close();
            }
        }
    }
}
