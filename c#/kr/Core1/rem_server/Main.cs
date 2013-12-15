using System;
using Core;
using Core.Remoting;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace rem_server
{
	static class MainClass
	{
		public static void Main (string[] args)
		{
			int port = 0;
			port = args.Length > 0 && Int32.TryParse(args[0], out port) ? port : 1800;

			var srv = new Server(port);

			Console.WriteLine("SearcherHost is ready\nListen port: {0}",port);

			var str = string.Empty;
			foreach(var i in srv.Tcp.GetUrlsForUri("Searcher"))
				str += i + "\n";

			Console.WriteLine("uri {0}", str);
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();

		}


	}
}
