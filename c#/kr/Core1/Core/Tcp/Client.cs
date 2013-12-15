using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Core.Tcp
{
	public class Client
	{
		/// <summary>
		/// The main socket.
		/// </summary>
		private Socket mainSocket;

		/// <summary>
		/// The ip endpoint.
		/// </summary>
		private IPEndPoint ipEndpoint;

		/// <summary>
		/// Initializes a new instance of the <see cref="Core.Tcp.Client"/> class.
		/// </summary>
		/// <param name='host'>
		/// Host.
		/// </param>
		/// <param name='port'>
		/// Port.
		/// </param>
		public Client (string host, int port)
		{
			host = host.Trim();

			if (string.IsNullOrEmpty(host))
				return;

			IPAddress ipAddress =
      			Dns.GetHostAddresses (host) [0];
			ipEndpoint =
      			new IPEndPoint (ipAddress, port);
			
			mainSocket = new Socket (ipEndpoint.AddressFamily,
		    	SocketType.Stream,
		        ProtocolType.Tcp);
		}

		public bool Connect() {
			if(mainSocket == null)
				return false;

			mainSocket.Connect(ipEndpoint);
			return mainSocket.Connected;
		}

		public bool Connected 
		{
			get {
				return mainSocket != null && mainSocket.Connected; 
			}
		}

		public IPEndPoint SocketEndPoint {
			get {
				return ipEndpoint;
			}
		}

		/// <summary>
		/// Receive this instance.
		/// 
		/// </summary>
		public string Receive() {
			if (!Connected)
				return string.Empty;

			var result = string.Empty;
			var bytes = new byte[1024];

			var byteRead = mainSocket.Receive(bytes);
			if (byteRead > 0)
				result = Encoding.ASCII.GetString(bytes, 0, byteRead);
			return result;
		}

		public int SendText(string str) {
			if (!Connected)
				return -1;
				
			return mainSocket.Send(Encoding.ASCII.GetBytes(str));;
		}
	}
}

