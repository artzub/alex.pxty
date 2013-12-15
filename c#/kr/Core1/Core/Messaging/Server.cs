using System;
using System.Messaging;
using System.Threading;

namespace Core.Messaging
{
	public class Server
	{
		public static string pathServer = "Label:SearcherServerMSMQ";
		public static string pathClient = "Label:SearcherClientMSMQ";

		private MessageQueue mqClient;
		private MessageQueue mqServer;
		private Searcher schr;

		public Server ()
		{
			mqServer = MessageQueue.Exists(pathServer) ? new MessageQueue(pathServer) : MessageQueue.Create(pathServer);
			mqServer.Formatter = new BinaryMessageFormatter();
			mqClient = MessageQueue.Exists(pathClient) ? new MessageQueue(pathClient) : MessageQueue.Create(pathClient);
			mqClient.Formatter = new BinaryMessageFormatter();
		}

		public void Start ()
		{
			var th = new Thread(Run);
			th.IsBackground = true;
			th.Start();
		}

		private void Run ()
		{
			Console.WriteLine("MQ Server path: {0}", mqServer.Path);
			Console.WriteLine("MQ Client path: {0}", mqClient.Path);
			Console.WriteLine("Waiting for message...");
			while (true) {
				if (!mqClient.Transactional) {
					Thread.Sleep(1000);
					continue;
				}

				var mqt = new MessageQueueTransaction();
				mqt.Begin ();
				var obj = mqClient.Receive (mqt);
				mqt.Commit();

				Console.WriteLine("{0}", obj);

				if (obj != null) {

					var str = obj.ToString();
					switch(str.Substring(0, 3)) {
						//:0:2
						case ":0:":
							schr = new Searcher(Convert.ToInt32(str.Replace(":0:", "")));						
							if (schr == null)								
								SendText(mqServer, "-1");
							else 
								SendText(mqServer, string.Format("Seacher made (step {0})", schr.Step));
						break;
						//:1:2,3,4,5
						case ":1:":
							//make
							if (schr == null) {
								SendText(mqServer, "-1");
								continue;
							}

							foreach(var item in str.Replace(":1:", "").Split(','))
								if (!string.IsNullOrEmpty(item))
									schr.AddItem(Convert.ToInt32(item));
							SendText(mqServer, string.Format("Seacher filled: {0}", schr));
						break;
						case ":2:":
							//run
							if (schr == null) {
								SendText(mqServer, "-1");
								continue;
							}
							
							schr.Fix();
							SendText(mqServer, string.Format("Seacher fixed: {0}", schr));
						break;

					}
					Console.WriteLine("Waiting for message...");
				}
			}
		}

		public static void SendText (MessageQueue mq,  string str)
		{
			var mqt = new MessageQueueTransaction();
			try {
				mqt.Begin();
				var msg = new Message(str);
				mq.Send (msg, mqt);
				mqt.Commit();

			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
				mqt.Abort();
			}
		}
	}
}

