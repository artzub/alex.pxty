using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;

namespace Core.Tcp
{
	public class Server
	{
		private Socket mainSocket;
		class ClientInfo {
			public Thread Thread{
				get; private set;
			}
			public Socket Socket {
				get;
				private set;
			}
			public ClientInfo(Thread t, Socket s) {
				Thread = t;
				Socket = s;
			}
		}



		public Server (string host, int port)
		{
			IPAddress ipAddress =
      			Dns.GetHostAddresses (host) [0];
			IPEndPoint ipEndpoint =
      			new IPEndPoint (ipAddress, port);
			
			mainSocket = new Socket (ipEndpoint.AddressFamily,
		    	SocketType.Stream,
		        ProtocolType.Tcp);

			mainSocket.Bind (ipEndpoint);
			Console.WriteLine ("Binded {0}", ipEndpoint);
			mainSocket.Listen ((int) SocketOptionName.MaxConnections);


		}

		public void Start () {
			while (true) {
				Console.WriteLine ("Wait for connection...");
				var socket = mainSocket.Accept ();

				var thread = new Thread (ConnetionWorker);
				thread.IsBackground = true;
				Console.WriteLine ("Connected client {1} ip = {0}", socket.RemoteEndPoint, thread.ManagedThreadId);
				thread.Start (new ClientInfo (thread, socket));
			}
		}

		private void ConnetionWorker (object obj)
		{
			var clientInfo = obj as ClientInfo;

			if (clientInfo == null)
				return;
		
			var buffer = new byte[1024];
			var readBytes = 0;
			var str = string.Empty;
			Searcher schr = null;

			try {
				if (clientInfo.Socket != null && clientInfo.Thread != null && clientInfo.Thread.IsAlive)
					SendText(clientInfo.Socket, string.Format("Your id is {0}", clientInfo.Thread.ManagedThreadId));

				while (clientInfo.Socket != null && clientInfo.Thread != null && clientInfo.Thread.IsAlive ) {
					readBytes = clientInfo.Socket.Receive (buffer);
					if (readBytes == 0)
						break;
					str = Encoding.ASCII.GetString (buffer, 0, readBytes);
					Console.WriteLine ("Client {0} said: {1}", clientInfo.Thread.ManagedThreadId ,str);
					switch(str.Substring(0, 3)) {
						//:0:2
						case ":0:":
							schr = new Searcher(Convert.ToInt32(str.Replace(":0:", "")));						
							if (schr == null)
								SendText(clientInfo.Socket, "-1");
							else 
								SendText(clientInfo.Socket, string.Format("Seacher made (step {0})", schr.Step));
						break;
						//:1:2,3,4,5
						case ":1:":
							//make
							if (schr == null) {
								SendText(clientInfo.Socket, "-1");
								continue;
							}

							foreach(var item in str.Replace(":1:", "").Split(','))
								if (!string.IsNullOrEmpty(item))
									schr.AddItem(Convert.ToInt32(item));
							SendText(clientInfo.Socket, string.Format("Seacher filled: {0}", schr));
						break;
						case ":2:":
							//run
							if (schr == null) {
								SendText(clientInfo.Socket, "-1");
								continue;
							}
							
							schr.Fix();
							SendText(clientInfo.Socket, string.Format("Seacher fixed: {0}", schr));
						break;

					}
				}
			} catch(Exception ex) {
				Console.WriteLine("Client {0} Error: {1}",  clientInfo.Thread.ManagedThreadId, ex.Message);
			}

		}

		private int SendText(Socket s, string str) {
			return s.Send(Encoding.ASCII.GetBytes(str));
		}
	}
}

