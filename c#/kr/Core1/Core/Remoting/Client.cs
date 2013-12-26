using System;
using Core;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;


namespace Core.Remoting
{
	public class Client
	{
		ISearcher sch;
		public bool Enable{ get; private set; }  

		public Client (string host = "localhost", int port = 1800, int step = 2)
		{
			try {
				var tcp = new TcpClientChannel ("Searcher", null);
				ChannelServices.RegisterChannel (tcp, false);
				sch = (ISearcher)Activator.GetObject (typeof(ISearcher), string.Format("tcp://{0}:{1}/Searcher", host, port), 2);

				Enable = sch != null;
			} catch (Exception) {
			}
		}

		public void InitStep(int step) 
		{
			sch.Step = step;
		}

		public bool Add (int item)
		{
			return Enable && sch.AddItem(item) > -1;
		}

		public bool Fix ()
		{
			return Enable && sch.Fix();
		}

		public override string ToString ()
		{
			return string.Format ("{0}", Enable ? sch.ItemsToString() : "Not initialized");
		}
	}
}

