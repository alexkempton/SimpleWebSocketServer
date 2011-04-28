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
                byte[] bytes = new byte[500];
                int numberOfBytesRead = 0;

                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                   using(NetworkStream stream = client.GetStream())
                    {
					

						
                       numberOfBytesRead = numberOfBytesRead + stream.Read(bytes,0,bytes.Length);
						byte[] handshakeCode = handshake.HandleClientHandshake(bytes, numberOfBytesRead);
						byte[] handshakeResponse = handshake.GetHandShakeResponse(handshakeCode);
						stream.Write(handshakeResponse,0,handshakeResponse.Length);
		              }				
				bytes = new byte[500];		
				numberOfBytesRead = 0;
					
          
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
