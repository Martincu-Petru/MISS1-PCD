using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var argumentsParser = new ArgumentsParser(args);

            if (argumentsParser.CheckArgumentsLength() == false)
            {
                Console.WriteLine("Invalid parameters.");
                return;
            }

            var port = argumentsParser.GetPort();
            var isStopAndWait = argumentsParser.IsStopAndWait();

            Console.WriteLine($"Starting TCP and UDP servers on port {port}...");

            try
            {
                var udpServer = new UdpClient(port);
                var tcpServer = new TcpListener(IPAddress.Any, port);

                var udpThread = new Thread(() => UdpServerConnector.Process(udpServer, isStopAndWait))
                {
                    IsBackground = true, Name = "UDP server thread"
                };
                udpThread.Start();

                var tcpThread = new Thread(() => TcpServerConnector.Process(tcpServer, isStopAndWait))
                {
                    IsBackground = true, Name = "TCP server thread"
                };
                tcpThread.Start();

                Console.WriteLine("Press <ENTER> to stop the servers.");
                Console.ReadLine();

                TcpServerConnector.CloseServer();
                UdpServerConnector.CloseServer();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Main exception: " + ex);
            }
        }
    }
}