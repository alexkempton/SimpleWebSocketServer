using System;
using System.Text;
using System.Text.RegularExpressions;
namespace WebSockets
{
	public class Handshake
	{
	
		
			public void HandleClientHandshake(char[] clientHandshake, int numberOfBytesRead){
                   char[] last8Bits = new char[8];
                   Array.Copy(clientHandshake, (numberOfBytesRead-8), last8Bits, 0, 8);
                   string clientHandshakeString = new string(clientHandshake);

                   Match key1 = Regex.Match(clientHandshakeString,"Key1:(.*?\r)");
                   Match key2 = Regex.Match(clientHandshakeString, "Key2:(.*?\r)");

                   MatchCollection key1DigitCollection = Regex.Matches(key1.Groups[1].ToString(),"[0-9]");
                   StringBuilder key1Digits = new StringBuilder(); 
                    foreach(Match m in key1DigitCollection){
                        key1Digits.Append(m.ToString());
                    }
                   
    
                
                    Console.WriteLine("Received");
              
                   
                    Console.WriteLine(clientHandshakeString);
                    Console.WriteLine(int.Parse(key1Digits.ToString()));
                    
              
                    numberOfBytesRead = 0;
			
			}
			
		
	}
}

