﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sanatana.Notifications.Service
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DeliveryAddress", Namespace="http://schemas.datacontract.org/2004/07/Sanatana.Notifications.DAL.Entities")]
    public partial class DeliveryAddress : object
    {
        
        private string AddressField;
        
        private int DeliveryTypeField;
        
        private string LanguageField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address
        {
            get
            {
                return this.AddressField;
            }
            set
            {
                this.AddressField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int DeliveryType
        {
            get
            {
                return this.DeliveryTypeField;
            }
            set
            {
                this.DeliveryTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Language
        {
            get
            {
                return this.LanguageField;
            }
            set
            {
                this.LanguageField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SignalDispatchOflong", Namespace="http://schemas.datacontract.org/2004/07/Sanatana.Notifications.DAL.Entities")]
    public partial class SignalDispatchOflong : object
    {
        
        private System.Nullable<int> CategoryIdField;
        
        private System.DateTime CreateDateUtcField;
        
        private int DeliveryTypeField;
        
        private int FailedAttemptsField;
        
        private bool IsScheduledField;
        
        private string ReceiverAddressField;
        
        private System.Nullable<long> ReceiverSubscriberIdField;
        
        private System.Nullable<int> ScheduleSetField;
        
        private System.DateTime SendDateUtcField;
        
        private long SignalDispatchIdField;
        
        private string TopicIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> CategoryId
        {
            get
            {
                return this.CategoryIdField;
            }
            set
            {
                this.CategoryIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateDateUtc
        {
            get
            {
                return this.CreateDateUtcField;
            }
            set
            {
                this.CreateDateUtcField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int DeliveryType
        {
            get
            {
                return this.DeliveryTypeField;
            }
            set
            {
                this.DeliveryTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FailedAttempts
        {
            get
            {
                return this.FailedAttemptsField;
            }
            set
            {
                this.FailedAttemptsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsScheduled
        {
            get
            {
                return this.IsScheduledField;
            }
            set
            {
                this.IsScheduledField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReceiverAddress
        {
            get
            {
                return this.ReceiverAddressField;
            }
            set
            {
                this.ReceiverAddressField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> ReceiverSubscriberId
        {
            get
            {
                return this.ReceiverSubscriberIdField;
            }
            set
            {
                this.ReceiverSubscriberIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ScheduleSet
        {
            get
            {
                return this.ScheduleSetField;
            }
            set
            {
                this.ScheduleSetField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime SendDateUtc
        {
            get
            {
                return this.SendDateUtcField;
            }
            set
            {
                this.SendDateUtcField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long SignalDispatchId
        {
            get
            {
                return this.SignalDispatchIdField;
            }
            set
            {
                this.SignalDispatchIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TopicId
        {
            get
            {
                return this.TopicIdField;
            }
            set
            {
                this.TopicIdField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Sanatana.Notifications.Service.ISignalServiceOf_Int64")]
    public interface ISignalServiceOf_Int64
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISignalServiceOf_Int64/RaiseEventAndMatchSubscribers", ReplyAction="http://tempuri.org/ISignalServiceOf_Int64/RaiseEventAndMatchSubscribersResponse")]
        System.Threading.Tasks.Task RaiseEventAndMatchSubscribersAsync(System.Collections.Generic.Dictionary<string, string> data, int categoryId, string topicId, System.Nullable<long> subscribersGroupId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISignalServiceOf_Int64/RaiseEventForSubscribersDirectly", ReplyAction="http://tempuri.org/ISignalServiceOf_Int64/RaiseEventForSubscribersDirectlyRespons" +
            "e")]
        System.Threading.Tasks.Task RaiseEventForSubscribersDirectlyAsync(System.Collections.Generic.List<long> subscriberIds, System.Collections.Generic.Dictionary<string, string> data, int categoryId, string topicId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISignalServiceOf_Int64/RaiseEventForAddressesDirectly", ReplyAction="http://tempuri.org/ISignalServiceOf_Int64/RaiseEventForAddressesDirectlyResponse")]
        System.Threading.Tasks.Task RaiseEventForAddressesDirectlyAsync(System.Collections.Generic.List<Sanatana.Notifications.Service.DeliveryAddress> deliveryAddresses, System.Collections.Generic.Dictionary<string, string> data, int categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISignalServiceOf_Int64/SendDispatch", ReplyAction="http://tempuri.org/ISignalServiceOf_Int64/SendDispatchResponse")]
        System.Threading.Tasks.Task SendDispatchAsync(Sanatana.Notifications.Service.SignalDispatchOflong dispatch);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface ISignalServiceOf_Int64Channel : Sanatana.Notifications.Service.ISignalServiceOf_Int64, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class SignalServiceOf_Int64Client : System.ServiceModel.ClientBase<Sanatana.Notifications.Service.ISignalServiceOf_Int64>, Sanatana.Notifications.Service.ISignalServiceOf_Int64
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SignalServiceOf_Int64Client() : 
                base(SignalServiceOf_Int64Client.GetDefaultBinding(), SignalServiceOf_Int64Client.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.MainTcpEndpoint.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SignalServiceOf_Int64Client(EndpointConfiguration endpointConfiguration) : 
                base(SignalServiceOf_Int64Client.GetBindingForEndpoint(endpointConfiguration), SignalServiceOf_Int64Client.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SignalServiceOf_Int64Client(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SignalServiceOf_Int64Client.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SignalServiceOf_Int64Client(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SignalServiceOf_Int64Client.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SignalServiceOf_Int64Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task RaiseEventAndMatchSubscribersAsync(System.Collections.Generic.Dictionary<string, string> data, int categoryId, string topicId, System.Nullable<long> subscribersGroupId)
        {
            return base.Channel.RaiseEventAndMatchSubscribersAsync(data, categoryId, topicId, subscribersGroupId);
        }
        
        public System.Threading.Tasks.Task RaiseEventForSubscribersDirectlyAsync(System.Collections.Generic.List<long> subscriberIds, System.Collections.Generic.Dictionary<string, string> data, int categoryId, string topicId)
        {
            return base.Channel.RaiseEventForSubscribersDirectlyAsync(subscriberIds, data, categoryId, topicId);
        }
        
        public System.Threading.Tasks.Task RaiseEventForAddressesDirectlyAsync(System.Collections.Generic.List<Sanatana.Notifications.Service.DeliveryAddress> deliveryAddresses, System.Collections.Generic.Dictionary<string, string> data, int categoryId)
        {
            return base.Channel.RaiseEventForAddressesDirectlyAsync(deliveryAddresses, data, categoryId);
        }
        
        public System.Threading.Tasks.Task SendDispatchAsync(Sanatana.Notifications.Service.SignalDispatchOflong dispatch)
        {
            return base.Channel.SendDispatchAsync(dispatch);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.MainTcpEndpoint))
            {
                System.ServiceModel.NetTcpBinding result = new System.ServiceModel.NetTcpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.Security.Mode = System.ServiceModel.SecurityMode.None;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.MainTcpEndpoint))
            {
                return new System.ServiceModel.EndpointAddress("net.tcp://localhost:8810/Sanatana.NotificationsService/");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return SignalServiceOf_Int64Client.GetBindingForEndpoint(EndpointConfiguration.MainTcpEndpoint);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return SignalServiceOf_Int64Client.GetEndpointAddress(EndpointConfiguration.MainTcpEndpoint);
        }
        
        public enum EndpointConfiguration
        {
            
            MainTcpEndpoint,
        }
    }
}
