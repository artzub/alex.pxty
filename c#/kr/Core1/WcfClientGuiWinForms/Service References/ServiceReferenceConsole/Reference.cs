﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfClientGuiWinForms.ServiceReferenceConsole {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceConsole.ISchrService")]
    public interface ISchrService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchrService/AddItem", ReplyAction="http://tempuri.org/ISchrService/AddItemResponse")]
        string AddItem(int item);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchrService/Fix", ReplyAction="http://tempuri.org/ISchrService/FixResponse")]
        string Fix();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchrService/GetState", ReplyAction="http://tempuri.org/ISchrService/GetStateResponse")]
        string GetState();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchrService/Init", ReplyAction="http://tempuri.org/ISchrService/InitResponse")]
        string Init(int step);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISchrServiceChannel : WcfClientGuiWinForms.ServiceReferenceConsole.ISchrService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SchrServiceClient : System.ServiceModel.ClientBase<WcfClientGuiWinForms.ServiceReferenceConsole.ISchrService>, WcfClientGuiWinForms.ServiceReferenceConsole.ISchrService {
        
        public SchrServiceClient() {
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
    }
}
