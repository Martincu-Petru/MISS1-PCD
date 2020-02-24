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
        public void Connect(string server, string dataLocation, int port)
        {
            try
            {
                var client = new UdpClient();
                var endPoint = new IPEndPoint(IPAddress.Parse(server), port);
                var timer = new Stopwatch();
                var numberOfMessages = 0;
                ulong numberOfBytes = 0;

                timer.Start();
                client.Connect(endPoint);
                timer.Stop();

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
                        numberOfMessages++;
                        numberOfBytes += ulong.Parse(arrData.Length.ToString());

                        timer.Start();
                        client.Send(arrData, arrData.Length);
                        timer.Stop();
                    }
                }

                Console.WriteLine("Elapsed time: {0}", timer.Elapsed.ToString());
                Console.WriteLine("Number of messages: {0}", numberOfMessages);
                Console.WriteLine("Number of bytes: {0}", numberOfBytes);

                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
    }
}
