using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace stage_first {
    public class Client {
        Socket socket;
        IPEndPoint endPoint;

        public Client(string addressServer, int port = 0) {

            if (port <= 0)
                port = 8888;

            endPoint = new IPEndPoint(Dns.GetHostAddresses(addressServer)[0], port);

            socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public bool Connect() {
            
            socket.Connect(endPoint);
            Console.WriteLine("connected to {0}", socket.RemoteEndPoint);
            SendMsg("dfdfdf");
            return socket.Connected;
        }

        public bool Disconnect() {
            socket.Disconnect(true);
            return !socket.Connected;
        }

        ~Client() {
            if (socket != null) {
                //socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }


        public void SendMsg(string msg) {
            var bytes = new byte[1024];
            try {
                if (!socket.Connected)
                    if (!Connect())
                        throw new Exception("Проблемы с подключением!");

                var msgbs = Encoding.UTF8.GetBytes(msg);
                int bytesSent = socket.Send(msgbs);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public string ReciveMsg() {
            var bytes = new byte[1024];
            try {
                int bytesRec = 0;
                if (socket.Connected)
                    bytesRec = socket.Receive(bytes);
                return Encoding.UTF8.GetString(bytes, 0, bytesRec);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
