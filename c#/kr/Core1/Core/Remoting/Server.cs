using System;
using Core;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;


namespace Core
{
	public class Server
	{
		public TcpChannel Tcp {
			get;
			private set;
		}
		public Server (int port = 1800)
		{
			System.Collections.IDictionary dict = 
			    new System.Collections.Hashtable();
			dict["port"] = port;
			dict["authenticationMode"] = "IdentifyCallers";

			Tcp = new TcpChannel(dict, null, null);
			ChannelServices.RegisterChannel(Tcp, false);

			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(Searcher),
				"Searcher", 
				WellKnownObjectMode.Singleton
			);
			RemotingConfiguration.ApplicationName = "Searcher";
		}
	}
}

