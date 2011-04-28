using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Linq;

namespace WebSockets
	
{
	public class Handshake
	{
		
		
		
			public byte[] HandleClientHandshake(byte[] clientHandshake, int numberOfBytesRead){
                   byte[] last8Bytes = new byte[8];
					
                   Array.Copy(clientHandshake, (numberOfBytesRead-8), last8Bytes, 0, 8);
					
                   	string clientHandshakeString = System.Text.Encoding.UTF8.GetString(clientHandshake, 0, numberOfBytesRead);
			
					Key key1 = concateKeyNumbers(clientHandshakeString, "Key1");
					Key key2 = concateKeyNumbers(clientHandshakeString, "Key2");
					
					
			
					
					byte[] returnKeyInBytes = new byte[key1.Bytes.Length + key2.Bytes.Length];
					Array.Copy(key1.Bytes, returnKeyInBytes, (key1.Bytes.Length));
					Array.Copy(key2.Bytes,0,returnKeyInBytes,(key1.Bytes.Length),(key2.Bytes.Length));
						
				
					byte[] returnBytes = new byte[returnKeyInBytes.Length + 8];
					Array.Copy(returnKeyInBytes, returnBytes, (returnKeyInBytes.Length));
					Array.Copy(last8Bytes,0,returnBytes,(returnKeyInBytes.Length),8);
					
					MD5 md5Hasher = MD5.Create();

    			    byte[] data = md5Hasher.ComputeHash(returnBytes);

                    Console.WriteLine("Received");
                    Console.WriteLine(clientHandshakeString);
                  
					
                    
              
                    numberOfBytesRead = 0;	
			return data;
			}
		
		
			Key concateKeyNumbers(string clientHandshakeString, string keyName){
				Key key = new Key();
				Match keyMatch = Regex.Match(clientHandshakeString, keyName+":\\s(.+)\r\n");
	
			
			string k = Regex.Replace(keyMatch.Groups[1].ToString(), "[^0-9]", "");
			
			key.Keynumbers = Int64.Parse(k);
			
			key.NumberOfSpaces = keyMatch.Groups[1].ToString().Count(c => c == ' ');
			
				
			key.Value = key.Keynumbers/key.NumberOfSpaces;
			
			key.Bytes = BitConverter.GetBytes((Int32)(key.Value));
      
          
             Array.Reverse(key.Bytes);
               
            
			
			return key;
		}
		
		
	public byte[] GetHandShakeResponse (byte[] handshakeCode)
		{
		string handshakeResponse =
       "HTTP/1.1 101 WebSocket Protocol Handshake\r\n" +
       "Upgrade: WebSocket\r\n" +
       "Connection: Upgrade\r\n" +
       "Sec-WebSocket-Origin: http://localhost:8888\r\n" +
       "Sec-WebSocket-Location: ws://localhost:8181/websession\r\n" +
	//	"Sec-WebSocket-Protocol: sample\r\n" +
       "\r\n";
			
		
			
		byte[] inBytes = System.Text.Encoding.UTF8.GetBytes(handshakeResponse);	
		byte[] fullHandshakeResponse = new byte[inBytes.Length + handshakeCode.Length];
		Array.Copy(inBytes, fullHandshakeResponse, (inBytes.Length));
		Array.Copy(handshakeCode,0,fullHandshakeResponse,(inBytes.Length),(handshakeCode.Length));
		
			return fullHandshakeResponse;
		}
			

		
	}
	
	
	
	
	
	public struct Key {
		public Int64 Value {get;set;}
		public Int64 Keynumbers {get;set; }
		public int NumberOfSpaces {get;set;}
		public byte[] Bytes {get; set;}
	}
	
	

	
}

