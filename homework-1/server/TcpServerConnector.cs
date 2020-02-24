using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace server
{
    internal static class TcpServerConnector
    {
        public static void Process(object arg)
        {
            Console.WriteLine("TCP server thread started");

            try
            {
                var server = (TcpListener) arg;
                server.Start();

                while(true)
                {
                    var client = server.AcceptTcpClient();

                    var t = new Thread(HandleClient);
                    t.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
        }

        public static void HandleClient(object obj)
        {
            var client = (TcpClient) obj;
            var numberOfMessages = 0;
            ulong numberOfBytes = 0;
            var buffer = new byte[65535];
            var timer = new Stopwatch();

            using (var stream = client.GetStream())
            {
                timer.Start();
                var count = stream.Read(buffer, 0, buffer.Length);
                timer.Stop();

                while (count != 0)
                {
                    numberOfMessages++;
                    numberOfBytes += ulong.Parse(count.ToString());
                    timer.Start();
                    count = stream.Read(buffer, 0, buffer.Length);
                    timer.Stop();
                }
            }
            client.Close();

            Console.WriteLine("Elapsed time: {0}", timer.Elapsed.ToString());
            Console.WriteLine("Number of messages: {0}", numberOfMessages);
            Console.WriteLine("Number of bytes: {0}", numberOfBytes);
            Console.WriteLine("Protocol: TCP");
        }
    }
}