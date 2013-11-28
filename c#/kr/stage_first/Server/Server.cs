using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stage_first {
    public class Server {
        private Socket socket;
        private IPEndPoint endPoint;
        private Thread mainThread;        
        private Dictionary<Socket, Thread> connections;

        private Dictionary<Socket, Thread> Connections {
            get {
                if (connections == null)
                    connections = new Dictionary<Socket, Thread>();
                return connections;
            }
        }

        public int Port {
            get;
            private set;
        }

        public IPAddress Address {
            get;
            private set;
        }

        public Server(int port) {
            if (port <= 0)
                port = 8888;

            Port = port;

            Address = Dns.GetHostAddresses("localhost")[0];
                //IPAddress.Any;

            endPoint = new IPEndPoint(Address, port);
        }

        public void Start() {
            InitServerSocket();
            //mainThread = new Thread(ConnectionAcceper);
            //mainThread.IsBackground = true;
            //mainThread.Start();
            ConnectionAcceper();
        }

        private void InitServerSocket() {
            // Создаем сокет
            socket = new Socket(
                Address.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            //Присоединяем его к адресу
            socket.Bind(endPoint);

            //Начинаем слушать
            socket.Listen((int)
                SocketOptionName.MaxConnections);
        }

        private void ConnectionAcceper() {
            while (true) {
                // принимаем подключение
                Console.WriteLine("Wait connection...");
                var aSocket = socket.Accept();

                // создаем поток для нового подключения
                var aThread = new Thread(ConnectionWorker);                
                aThread.IsBackground = true;
                aThread.Start(aSocket);

                Console.WriteLine("New connection {0}", aThread.ManagedThreadId);

                lock (Connections) 
                    Connections.Add(aSocket, aThread);
            }
        }

        private void ConnectionWorker(object obj) {
            var socket = obj as Socket;
            var buffer = new byte[1024];
            var str = string.Empty;

            try {
                if (socket == null)
                    throw new Exception("Socket is null");

                while (true) {
                    int bytesRead = socket.Receive(buffer);


                    if (bytesRead > 0) {                        
                        str = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        //socket.Send(Encoding.ASCII.GetBytes("ok"));
                        str = string.Format("{0} : {1}", Connections[socket].ManagedThreadId, str);
                        Console.WriteLine(str);

                        lock (Connections) {
                            foreach (var item in Connections) {
                                if (item.Key != socket) {
                                    item.Key.Send(Encoding.UTF8.GetBytes(str));
                                }
                            }
                        }
                    }
                    else if (bytesRead == 0)
                        return;
                }
            }
            catch (SocketException ex) {
                Console.WriteLine("Socket exception: " + ex.SocketErrorCode);
            }
            catch (Exception ex) {
                Console.WriteLine("Exception: " + ex);
            }
            finally {
                if (socket != null) {
                    socket.Close();
                    lock (Connections)
                        Connections.Remove(socket);
                }
            }
        }
    }
}
