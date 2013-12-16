using System;
using System.Messaging;
using System.Threading;
using System.Collections.Generic;

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
			var result = string.Empty;
            var msg = mqServer.Receive();

            if (msg != null)
                if (msg.Body is SearcherMessage) {
                    var smsg = msg.Body as SearcherMessage;
                    result = smsg.ToString(true);
                }
                else {
                    result = string.Format("{0}", msg.Body);
                }

			return result;
		}

        public void SendInit(int step) {
            var msg = new SearcherMessage() {
                Type = TypeSearcherMessage.Init,
                Message = step.ToString()
            };
            Send(msg);
        }

        public void SendAdd(IList<string> list) {
            string str = string.Empty;
            foreach (var item in list) {
                str += item + ',';
            }
            Send((new SearcherMessage() {
                Type = TypeSearcherMessage.Add,
                Message = str
            }));
        }

        public void SendFix() {
            Send(new SearcherMessage() {
                Type = TypeSearcherMessage.Fix
            });
        }

        public void Send(SearcherMessage msg) {
            Server.Send(mqClient, msg);
        }

		public void SendText(string str) {
			Server.SendText(mqClient, str);
		}
	}
}

