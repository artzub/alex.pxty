using System;
using System.Messaging;
using System.Threading;

namespace Core.Messaging
{
	public class Client
	{
		private MessageQueue mqClient;
		private MessageQueue mqServer;

		public Client ()
		{
			mqServer = MessageQueue.Exists(Server.pathServer) ? new MessageQueue(Server.pathServer) : MessageQueue.Create(Server.pathServer);
			mqServer.Formatter = new BinaryMessageFormatter();
			mqClient = MessageQueue.Exists(Server.pathClient) ? new MessageQueue(Server.pathClient) : MessageQueue.Create(Server.pathClient);
			mqClient.Formatter = new BinaryMessageFormatter();
		}

		public string Receive() {
			if (!mqServer.Transactional)
				return string.Empty;

			var result = string.Empty;

			var mqt = new MessageQueueTransaction();
			mqt.Begin ();
			var obj = mqClient.Receive (mqt);
			mqt.Commit();

			return string.Format("{0}", obj);
		}

		public void SendText(string str) {
			Server.SendText(mqClient, str);
		}
	}
}

