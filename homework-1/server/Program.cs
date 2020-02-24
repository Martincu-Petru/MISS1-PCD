using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            var argumentsParser = new ArgumentsParser(args);

            if (argumentsParser.CheckArgumentsLength() == false)
            {
                Console.WriteLine("Invalid parameters.");
                return;
            }

            var port = argumentsParser.GetPort();

            Console.WriteLine(string.Format("Starting TCP and UDP servers on port {0}...", port));

            try
            {
                var udpServer = new UdpClient(port);
                var tcpServer = new TcpListener(IPAddress.Any, port);

                var udpThread = new Thread(new ParameterizedThreadStart(UdpServerConnector.Process));
                udpThread.IsBackground = true;
                udpThread.Name = "UDP server thread";
                udpThread.Start(udpServer);

                var tcpThread = new Thread(new ParameterizedThreadStart(TcpServerConnector.Process));
                tcpThread.IsBackground = true;
                tcpThread.Name = "TCP server thread";
                tcpThread.Start(tcpServer);

                Console.WriteLine("Press <ENTER> to stop the servers.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Main exception: " + ex);
            }
        }
    }
}
