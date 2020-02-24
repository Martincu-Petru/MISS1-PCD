using System;
using System.Net.Sockets;
using System.Threading;

namespace client
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

            var server = argumentsParser.GetServerAddress();
            var dataLocation = argumentsParser.GetDataLocation();
            var port = argumentsParser.GetPort();
            var protocol = argumentsParser.GetProtocol();

            switch (protocol)
            {
                case 0:
                    var tcpClient = new TcpClientConnector();
                    tcpClient.Connect(server, dataLocation, port);
                    break;
                case 1:
                    var udpClient = new UdpClientConnector();
                    udpClient.Connect(server, dataLocation, port);
                    break;
                default:
                    Console.WriteLine("Invalid parameters.");
                    break;
            }
        }
    }
}
