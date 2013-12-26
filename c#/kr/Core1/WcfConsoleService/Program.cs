using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WcfServiceLibrary;
using System.ServiceModel.Description;

namespace WcfConsoleService {
    class Program {
        static void Main(string[] args) {
			var host = "http://localhost";
			var port = 1800;

			foreach (var item in args) {
				if (item.Contains("=")) {
					var list = item.Split('=');
					if (list.Length > 1)
						switch(list[0].Substring(1)) {
							//-host=127.0.0.1
							case "host": 
								host = list[1];
							break;
							case "port":
								port = Convert.ToUInt16(list[1]);
								break;
						}
				}
			}

			if (string.IsNullOrEmpty(host))
				host = "http://localhost";


			using (var sh = new ServiceHost(typeof(SchrService), new Uri(string.Format("{0}:{1}/SchrService", host, port)))) {
                //, new Uri("http://localhost:8888/SchrService")
                sh.AddServiceEndpoint(typeof(ISchrService), new WSHttpBinding(), "");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                sh.Description.Behaviors.Add(smb);

                sh.Open();

                var str = "";
                foreach (var item in sh.BaseAddresses) {
		            str += item.ToString() + ",";
	            }

                Console.WriteLine("Service is ready. {0}: {1}", sh.State, str);
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                sh.Close();
            }
        }
    }
}
