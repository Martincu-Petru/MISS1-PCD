using System;
using System.Collections.Generic;
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
                var client = new TcpClient(server, port);
                var stream = client.GetStream();

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
                        stream.Write(arrData, 0, arrData.Length);

                        Console.WriteLine("Sent: {0}", arrData);
                    }
                }

                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
    }
}
