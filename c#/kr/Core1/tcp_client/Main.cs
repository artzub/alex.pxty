using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcp_client
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IPAddress ipAddress =
      			Dns.GetHostAddresses ("localhost") [0];
			IPEndPoint ipEndpoint =
      			new IPEndPoint (ipAddress, 1800);
			
			Socket listenSocket =
		      new Socket (ipEndpoint.AddressFamily,
		                 SocketType.Stream,
		                 ProtocolType.Tcp);
			listenSocket.Connect (ipEndpoint);
			while (listenSocket.Connected) {
				listenSocket.Send(Encoding.ASCII.GetBytes(Console.ReadLine()));
				var buffer = new byte[1024];
				var readBytes = listenSocket.Receive(buffer);
				Console.WriteLine("Server said: {0}",Encoding.ASCII.GetString(buffer,0,readBytes));
			
			}
		}
	}
}
