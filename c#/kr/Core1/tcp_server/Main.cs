using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using Core.Tcp;

namespace tcp_server
{
	static class MainClass
	{

		public static void Main (string[] args)
		{

			var host = "localhost";
			var port = 1800;

			foreach (var item in args) {
				if (item.Contains("=")) {
					var list = item.Split('=');
					if (list.Length > 1)
						switch(list[0].Substring(1)) {
							//-host=127.0.0.1
							case "host": 
								host = list[1];
							break;
							case "port":
								port = Convert.ToUInt16(list[1]);
								break;
						}
				}
			}

			if (string.IsNullOrEmpty(host))
				host = "localhost";

			var server = new Server(host, port);
			server.Start();
		}
	}
}
