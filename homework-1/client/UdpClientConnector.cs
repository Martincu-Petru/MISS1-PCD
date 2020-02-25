using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client
{
    internal class UdpClientConnector
    {
        public void Connect(string server, string dataLocation, int port, bool isStopAndWait)
        {
            try
            {
                var client = new UdpClient();
                var endPoint = new IPEndPoint(IPAddress.Parse(server), port);
                var timer = new Stopwatch();
                var numberOfMessages = 0;
                ulong numberOfBytes = 0;
                var buffer = new byte[65535];

                client.Connect(endPoint);

                using (Stream objStream = File.OpenRead(dataLocation))
                {
                    // Read data from file until read position is not equals to length of file
                    
                    while (objStream.Position != objStream.Length)
                    {
                        // Read number of remaining bytes to read
                        var remainingBytes = objStream.Length - objStream.Position;
                        var arrData = remainingBytes > 64535 ? new byte[64535] : new byte[remainingBytes];

                        // Read data from file
                        objStream.Read(arrData, 0, arrData.Length);
                        timer.Start();
                        client.Send(arrData, arrData.Length);
                        timer.Stop();

                        // Console.WriteLine("Client sent data");

                        bool acknowledged = false;
                        IPEndPoint remoteEndpoint = null;

                        if (isStopAndWait)
                        {
                            while (!acknowledged)
                            {
                                buffer = client.Receive(ref remoteEndpoint);
                                // Console.WriteLine("Client received ACK: " + buffer.ToString());
                                if (System.Text.Encoding.ASCII.GetString(buffer).Substring(0, 3).Equals("ACK"))
                                {
                                    client.Send(Encoding.ASCII.GetBytes("ACK"), 3);
                                    acknowledged = true;
                                }
                            } 
                        }

                        numberOfMessages++;
                        numberOfBytes += ulong.Parse(arrData.Length.ToString());
                    }
                }
                client.Close();

                Console.WriteLine("Elapsed time: {0}", timer.Elapsed.ToString());
                Console.WriteLine("Number of messages: {0}", numberOfMessages);
                Console.WriteLine("Number of bytes: {0}", numberOfBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
    }
}
