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
        public void Connect(string server, string dataLocation, int port)
        {
            try
            {
                var timer = new Stopwatch();
                timer.Start();
                var client = new TcpClient(server, port);
                var stream = client.GetStream();
                timer.Stop();
                var numberOfMessages = 0;
                ulong numberOfBytes = 0;

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

                        numberOfMessages++;
                        numberOfBytes += ulong.Parse(arrData.Length.ToString());
                    }
                }
                timer.Start();
                stream.Close();
                client.Close();
                timer.Stop();

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
