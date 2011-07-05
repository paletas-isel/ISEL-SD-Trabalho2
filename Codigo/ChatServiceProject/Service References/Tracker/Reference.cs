﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.454
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatServiceProject.Tracker {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Theme", Namespace="http://schemas.datacontract.org/2004/07/CentralServiceProject")]
    [System.SerializableAttribute()]
    public partial class Theme : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/CentralServiceProject")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ChatServiceProject.Tracker.Theme[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ChatServiceProject.Tracker.Theme))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ChatServiceProject.Tracker.User[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.InvalidOperationException))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.SystemException))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Exception))]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object CallbackField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Uri ChatServiceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Callback {
            get {
                return this.CallbackField;
            }
            set {
                if ((object.ReferenceEquals(this.CallbackField, value) != true)) {
                    this.CallbackField = value;
                    this.RaisePropertyChanged("Callback");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Uri ChatService {
            get {
                return this.ChatServiceField;
            }
            set {
                if ((object.ReferenceEquals(this.ChatServiceField, value) != true)) {
                    this.ChatServiceField = value;
                    this.RaisePropertyChanged("ChatService");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Tracker.ICentralService", CallbackContract=typeof(ChatServiceProject.Tracker.ICentralServiceCallback))]
    public interface ICentralService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICentralService/GetThemes", ReplyAction="http://tempuri.org/ICentralService/GetThemesResponse")]
        ChatServiceProject.Tracker.Theme[] GetThemes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICentralService/LogOn", ReplyAction="http://tempuri.org/ICentralService/LogOnResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.InvalidOperationException), Action="http://tempuri.org/ICentralService/LogOnInvalidOperationExceptionFault", Name="InvalidOperationException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        ChatServiceProject.Tracker.User[] LogOn(string themeName, string userName, System.Uri address);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICentralService/LogOff", ReplyAction="http://tempuri.org/ICentralService/LogOffResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.InvalidOperationException), Action="http://tempuri.org/ICentralService/LogOffInvalidOperationExceptionFault", Name="InvalidOperationException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        void LogOff(string themeName, long id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICentralServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICentralService/OnUserJoined")]
        void OnUserJoined(ChatServiceProject.Tracker.User user);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICentralService/OnUserLeft")]
        void OnUserLeft(ChatServiceProject.Tracker.User user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICentralServiceChannel : ChatServiceProject.Tracker.ICentralService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CentralServiceClient : System.ServiceModel.DuplexClientBase<ChatServiceProject.Tracker.ICentralService>, ChatServiceProject.Tracker.ICentralService {
        
        public CentralServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public CentralServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public CentralServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CentralServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CentralServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public ChatServiceProject.Tracker.Theme[] GetThemes() {
            return base.Channel.GetThemes();
        }
        
        public ChatServiceProject.Tracker.User[] LogOn(string themeName, string userName, System.Uri address) {
            return base.Channel.LogOn(themeName, userName, address);
        }
        
        public void LogOff(string themeName, long id) {
            base.Channel.LogOff(themeName, id);
        }
    }
}
