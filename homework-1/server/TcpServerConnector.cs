using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace server
{
    internal static class TcpServerConnector
    {
        private static TcpListener Server { get; set; }
        private static ulong _numberOfMessages = 0;
        private static ulong _numberOfBytes = 0;
        private static bool _isStopAndWait;
        private static readonly Stopwatch Timer = new Stopwatch();
        public static void Process(object arg, bool isStopAndWait)
        {
            Console.WriteLine("TCP server thread started");

            _isStopAndWait = isStopAndWait;

            try
            {
                Server = (TcpListener) arg;
                Server.Start();

                var client = Server.AcceptTcpClient();
                var clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TCP server exception: " + ex);
            }
        }

        public static void HandleClient(object obj)
        {
            var client = (TcpClient) obj;
            
            var buffer = new byte[65535];

            try
            {
                using (var stream = client.GetStream())
                {
                    Timer.Start();
                    var count = stream.Read(buffer, 0, buffer.Length);
                    Timer.Stop();

                    _numberOfMessages++;
                    _numberOfBytes += ulong.Parse(count.ToString());

                    // Console.WriteLine(System.Text.Encoding.ASCII.GetString(buffer));
                    //Console.WriteLine("Server read data");

                    bool acknowledged;

                    if (_isStopAndWait)
                    {

                        acknowledged = false;

                        while (!acknowledged)
                        {
                            stream.Write(Encoding.ASCII.GetBytes("ACK"), 0, 3);
                            //Console.WriteLine("Server wrote ACK");
                            stream.Read(buffer, 0, buffer.Length);
                            if (System.Text.Encoding.ASCII.GetString(buffer).Substring(0, 3).Equals("ACK"))
                            {
                                acknowledged = true;
                            }
                        }
                    }

                    while (count != 0)
                    {
                        _numberOfMessages++;
                        _numberOfBytes += ulong.Parse(count.ToString());
                        Timer.Start();
                        count = stream.Read(buffer, 0, buffer.Length);
                        Timer.Stop();

                        if (_isStopAndWait)
                        {
                            acknowledged = false;

                            while (!acknowledged)
                            {
                                stream.Write(Encoding.ASCII.GetBytes("ACK"), 0, 3);
                                stream.Read(buffer, 0, buffer.Length);
                                if (System.Text.Encoding.ASCII.GetString(buffer).Substring(0, 3).Equals("ACK"))
                                {
                                    acknowledged = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("TCP client disconnected.");
            }
            finally
            {
                CloseServer();
            }
            client.Close();
        }

        public static void CloseServer()
        {
            Console.WriteLine();
            Console.WriteLine("Statistics: ");
            Console.WriteLine("Elapsed time: {0}", Timer.Elapsed.ToString());
            Console.WriteLine("Number of messages: {0}", _numberOfMessages);
            Console.WriteLine("Number of bytes: {0}", _numberOfBytes);
            Console.WriteLine("Protocol: TCP");

            Server.Server.Close();
        }
    }
}