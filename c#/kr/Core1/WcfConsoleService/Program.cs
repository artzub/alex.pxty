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
            using (var sh = new ServiceHost(typeof(SchrService), new Uri("http://localhost:8888/SchrService"))) {
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
