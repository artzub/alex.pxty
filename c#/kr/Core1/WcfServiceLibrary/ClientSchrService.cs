using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace WcfServiceLibrary {
    public partial class SchrServiceClient : ClientBase<ISchrService>, ISchrService {

        public enum TypeBinding {
            BaseHttp,
            WSHttp
        }

        public static SchrServiceClient GetClient(string uri, TypeBinding binding) {
            Binding b;

            if (binding == TypeBinding.WSHttp)
                b = new WSHttpBinding();
            else
                b = new BasicHttpBinding();

            var cd = ContractDescription.GetContract(typeof(ISchrService), typeof(SchrService));
            return new SchrServiceClient(new ServiceEndpoint(cd, b, new EndpointAddress(uri)));
        }

        public SchrServiceClient() {
        }

        public SchrServiceClient(ServiceEndpoint se):
            base(se) {
        }

        public SchrServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName) {
        }

        public SchrServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress) {
        }

        public SchrServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress) {
        }

        public SchrServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress) {
        }

        public string AddItem(int item) {
            return base.Channel.AddItem(item);
        }

        public string Fix() {
            return base.Channel.Fix();
        }

        public string GetState() {
            return base.Channel.GetState();
        }

        public string Init(int step) {
            return base.Channel.Init(step);
        }

        public override string ToString() {
            return string.Format("Service Contract: {0}; Endpoint: {1}", base.ToString(), Endpoint.Address);
        }
    }
}
