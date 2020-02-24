using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server
{
    internal static class UdpServerConnector
    {
        public static void Process(object arg)
        {
            Console.WriteLine("UDP server thread started");

            try
            {
                var server = (UdpClient)arg;

                for (; ; )
                {
                    IPEndPoint remoteEndpoint = null;
                    var buffer = server.Receive(ref remoteEndpoint);

                    if (buffer.Length > 0)
                    {
                        Console.WriteLine("UDP: " + Encoding.ASCII.GetString(buffer));
                    }
                }
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode != 10004)
                    Console.WriteLine("UDPServerProc exception: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UDPServerProc exception: " + ex);
            }

            Console.WriteLine("UDP server thread finished");
        }
    }
}
