using System;
using System.Net.Sockets;
using System.Text;

namespace server
{
    internal static class TcpServerConnector
    {
        public static void Process(object arg)
        {
            Console.WriteLine("TCP server thread started");

            try
            {
                var server = (TcpListener) arg;
                var buffer = new byte[65535];

                server.Start();

                for (;;)
                {
                    var client = server.AcceptTcpClient();

                    using (var stream = client.GetStream())
                    {
                        int count;
                        while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
                            Console.WriteLine("TCP: " + Encoding.ASCII.GetString(buffer, 0, count));
                    }

                    client.Close();
                }
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode != 10004) Console.WriteLine("TCPServerProc exception: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TCPServerProc exception: " + ex);
            }

            Console.WriteLine("TCP server thread finished");
        }
    }
}