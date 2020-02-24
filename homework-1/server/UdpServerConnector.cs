using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var timer = new Stopwatch();
            var numberOfMessages = 0;
            ulong numberOfBytes = 0;

            try
            {
                var server = (UdpClient)arg;

                for (; ; )
                {
                    IPEndPoint remoteEndpoint = null;
                    timer.Start();
                    var buffer = server.Receive(ref remoteEndpoint);
                    timer.Stop();

                    numberOfMessages++;
                    numberOfBytes += ulong.Parse(buffer.Length.ToString());

                    Console.WriteLine("Elapsed time: {0}", timer.Elapsed.ToString());
                    Console.WriteLine("Number of messages: {0}", numberOfMessages);
                    Console.WriteLine("Number of bytes: {0}", numberOfBytes);
                    Console.WriteLine("Protocol: UDP");
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

            Console.WriteLine("Elapsed time: {0}", timer.Elapsed.ToString());
            Console.WriteLine("Number of messages: {0}", numberOfMessages);
            Console.WriteLine("Number of bytes: {0}", numberOfBytes);
            Console.WriteLine("Protocol: UDP");

            Console.WriteLine("UDP server thread finished");
        }
    }
}
