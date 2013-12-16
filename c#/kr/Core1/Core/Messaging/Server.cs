using System;
using System.Messaging;
using System.Threading;

namespace Core.Messaging
{
	public class Server : ServerBase
	{
        public static string pathServer = ".\\Private$\\SearcherServerMSMQ";
        public static string pathClient = ".\\Private$\\SearcherClientMSMQ";

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
				var obj = mqClient.Receive();

				if (obj != null) {

                    var msg = SearcherMessage.Parse(obj.Body);

                    Console.WriteLine("{0}", msg);

                    SendText(mqServer, DoWork(msg, ref schr));
					
					Console.WriteLine("Waiting for message...");
				}
			}
		}

        public static void Send(MessageQueue mq, SearcherMessage msg) {
            try {
                mq.Send(msg);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

		public static void SendText(MessageQueue mq,  string str)
		{
			Send(mq, new SearcherMessage() { 
                Type = TypeSearcherMessage.None,
                Message = str
            });
		}
	}
}

