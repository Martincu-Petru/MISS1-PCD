using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace client
{
    internal class TcpClientConnector
    {
        public void Connect(string server, string dataLocation, int port, bool isStopAndWait)
        {
            try
            {
                var timer = new Stopwatch();
                var client = new TcpClient(server, port);
                var stream = client.GetStream();
                var numberOfMessages = 0;
                ulong numberOfBytes = 0;
                var buffer = new byte[65535];

                using (Stream objStream = File.OpenRead(dataLocation))
                {
                    // Read data from file until read position is not equals to length of file
                    while (objStream.Position != objStream.Length)
                    {
                        // Read number of remaining bytes to read
                        var remainingBytes = objStream.Length - objStream.Position;
                        var arrData = remainingBytes > 65535 ? new byte[65535] : new byte[remainingBytes];

                        // Read data from file
                        objStream.Read(arrData, 0, arrData.Length);
                        timer.Start();
                        stream.Write(arrData, 0, arrData.Length);
                        timer.Stop();

                        // Console.WriteLine("Client sent data");

                        bool acknowledged = false;

                        if (isStopAndWait)
                        {
                            while (!acknowledged)
                            {
                                stream.Read(buffer, 0, buffer.Length);
                                if (System.Text.Encoding.ASCII.GetString(buffer).Substring(0, 3).Equals("ACK"))
                                {
                                    // Console.WriteLine("Client received ACK: " + System.Text.Encoding.ASCII.GetString(buffer));
                                    stream.Write(Encoding.ASCII.GetBytes("ACK"), 0, 3);
                                    acknowledged = true;
                                }
                            }
                        }

                        numberOfMessages++;
                        numberOfBytes += ulong.Parse(arrData.Length.ToString());
                    }
                }
                stream.Close();
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
