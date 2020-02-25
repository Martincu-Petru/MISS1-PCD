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
        private static UdpClient Server { get; set; }
        private static ulong _numberOfMessages = 0;
        private static ulong _numberOfBytes = 0;
        private static readonly Stopwatch Timer = new Stopwatch();
        public static void Process(object arg, bool isStopAndWait)
        {
            Console.WriteLine("UDP server thread started");

            try
            {
                Server = (UdpClient)arg;
                bool acknowledged = false;
                IPEndPoint remoteEndpoint = null;

                while (true)
                {
                    
                    Timer.Start();
                    var buffer = Server.Receive(ref remoteEndpoint);
                    Timer.Stop();

                    _numberOfMessages++;
                    _numberOfBytes += ulong.Parse(buffer.Length.ToString());

                    if(isStopAndWait) 
                    { 

                        while (!acknowledged)
                        {
                            Server.Send(Encoding.ASCII.GetBytes("ACK"), 3, remoteEndpoint);
                            buffer = Server.Receive(ref remoteEndpoint);
                            if (System.Text.Encoding.ASCII.GetString(buffer).Substring(0, 3).Equals("ACK"))
                            {
                                acknowledged = true;
                            }
                        }

                        acknowledged = false;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("UDP server exception: " + exception);
            }
            Console.WriteLine("UDP server thread finished");
        }

        public static void CloseServer()
        {
            Console.WriteLine();
            Console.WriteLine("Elapsed time: {0}", Timer.Elapsed.ToString());
            Console.WriteLine("Number of messages: {0}", _numberOfMessages);
            Console.WriteLine("Number of bytes: {0}", _numberOfBytes);
            Console.WriteLine("Protocol: UDP");

            Server.Close();
        }
    }
}
