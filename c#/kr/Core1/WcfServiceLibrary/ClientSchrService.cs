using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WcfServiceLibrary {
    public class Client : ClientBase<ISchrService>, ISchrService {

        public Client() {

        }

        public string AddItem(int item) {
            return Channel.AddItem(item);
        }

        public string Fix() {
            return Channel.Fix();
        }

        public string GetState() {
            return Channel.GetState();
        }

        public string Init(int step) {
            return Channel.Init(step);
        }
    }
}
