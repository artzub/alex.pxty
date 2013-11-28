using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stage_first {
    class Program {
        static void Main(string[] args) {
            var cl = new Client("localhost", 8080);
            if (cl.Connect()) {
                var t1 = new Thread(Reader);
                t1.IsBackground = true;
                t1.Start(cl);

                while (true) {
                    Writer(cl);
                }
            }
        }

        static private void Writer(object sndr) {
            var cl = sndr as Client;
            var str = string.Empty;
            
            lock (Console.In)
                str = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(str))
                cl.SendMsg(str);
            
        }

        static private void Reader(object sndr) {
            var cl = sndr as Client;
            var str = string.Empty;

            while (true) {
                str = cl.ReciveMsg();
                if (!string.IsNullOrWhiteSpace(str))
                    lock (Console.Out)
                        Console.WriteLine(str);
            }
        }
    }
}
