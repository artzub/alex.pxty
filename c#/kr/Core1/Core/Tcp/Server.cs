using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;

namespace Core.Tcp
{
	public class Server : ServerBase
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

                    var msg = SearcherMessage.Parse(str);

                    if (msg == null)
                        continue;

                    SendText(clientInfo.Socket, DoWork(msg, ref schr));
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

