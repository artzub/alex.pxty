using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stage_first {
    class Program {

        static void Main(string[] args) {
            var port = string.Empty;
            
            try {
                if (args.Length > 1) {
                    foreach (var item in args) {
                        switch (item.Substring(1).Split('=')[0]) {
                            case "port":
                                port = (item.Split('=')[1] ?? port).Trim();
                                break;                            
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(port))
                    port = "8080";

                var server = new Server(Int32.Parse(port));

                Console.WriteLine("Server connect info: {0}:{1}", server.Address, server.Port);

                server.Start();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
