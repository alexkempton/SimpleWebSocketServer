using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;


namespace WebSockets
{
    class Program
    {
        public static void Main()
		
        {
			Handshake handshake = new Handshake();
            TcpListener server = null;
            try
            {
				server = new TcpListener(IPAddress.Loopback, 8181);
                server.Start();
                char[] bytes = new char[256];
                int numberOfBytesRead = 0;

                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                   using(NetworkStream stream = client.GetStream())
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.Default, true))
                    {
                        numberOfBytesRead = numberOfBytesRead + streamReader.Read(bytes,0,255);
                      
                    }
					
					handshake.HandleClientHandshake(bytes, numberOfBytesRead);
				
          
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        
    }
}
