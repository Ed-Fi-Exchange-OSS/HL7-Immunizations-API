﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wi.Dpi.Immunizations.Dhs.Wir
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:cdc:iisb:2011")]
    public partial class soapFaultType
    {
        
        private string codeField;
        
        private string reasonField;
        
        private string detailField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=0)]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                this.reasonField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:cdc:iisb:2011")]
    public partial class UnsupportedOperationFaultType
    {
        
        private string codeField;
        
        private object reasonField;
        
        private string detailField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=0)]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        // CODEGEN Warning: 'fixed' attribute supported only for primitive types.  Ignoring fixed='UnsupportedOperation' attribute.
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public object Reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                this.reasonField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:cdc:iisb:2011")]
    public partial class MessageTooLargeFaultType
    {
        
        private string codeField;
        
        private object reasonField;
        
        private string detailField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=0)]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        // CODEGEN Warning: 'fixed' attribute supported only for primitive types.  Ignoring fixed='MessageTooLarge' attribute.
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public object Reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                this.reasonField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:cdc:iisb:2011")]
    public partial class SecurityFaultType
    {
        
        private string codeField;
        
        private object reasonField;
        
        private string detailField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=0)]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        // CODEGEN Warning: 'fixed' attribute supported only for primitive types.  Ignoring fixed='Security' attribute.
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public object Reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                this.reasonField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:cdc:iisb:2011", ConfigurationName="Wi.Dpi.Immunizations.Dhs.Wir.IIS_PortType")]
    public interface IIS_PortType
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cdc:iisb:2011:connectivityTest", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(Wi.Dpi.Immunizations.Dhs.Wir.soapFaultType), Action="urn:cdc:iisb:2011:connectivityTest", Name="fault")]
        [System.ServiceModel.FaultContractAttribute(typeof(Wi.Dpi.Immunizations.Dhs.Wir.UnsupportedOperationFaultType), Action="urn:cdc:iisb:2011:connectivityTest", Name="UnsupportedOperationFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<Wi.Dpi.Immunizations.Dhs.Wir.connectivityTestResponse> connectivityTestAsync(Wi.Dpi.Immunizations.Dhs.Wir.connectivityTestRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cdc:iisb:2011:submitSingleMessage", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(Wi.Dpi.Immunizations.Dhs.Wir.soapFaultType), Action="urn:cdc:iisb:2011:submitSingleMessage", Name="fault")]
        [System.ServiceModel.FaultContractAttribute(typeof(Wi.Dpi.Immunizations.Dhs.Wir.MessageTooLargeFaultType), Action="urn:cdc:iisb:2011:submitSingleMessage", Name="MessageTooLargeFault")]
        [System.ServiceModel.FaultContractAttribute(typeof(Wi.Dpi.Immunizations.Dhs.Wir.SecurityFaultType), Action="urn:cdc:iisb:2011:submitSingleMessage", Name="SecurityFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<Wi.Dpi.Immunizations.Dhs.Wir.submitSingleMessageResponse> submitSingleMessageAsync(Wi.Dpi.Immunizations.Dhs.Wir.submitSingleMessageRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="connectivityTest", WrapperNamespace="urn:cdc:iisb:2011", IsWrapped=true)]
    public partial class connectivityTestRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:cdc:iisb:2011", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string echoBack;
        
        public connectivityTestRequest()
        {
        }
        
        public connectivityTestRequest(string echoBack)
        {
            this.echoBack = echoBack;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="connectivityTestResponse", WrapperNamespace="urn:cdc:iisb:2011", IsWrapped=true)]
    public partial class connectivityTestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:cdc:iisb:2011", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string @return;
        
        public connectivityTestResponse()
        {
        }
        
        public connectivityTestResponse(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="submitSingleMessage", WrapperNamespace="urn:cdc:iisb:2011", IsWrapped=true)]
    public partial class submitSingleMessageRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:cdc:iisb:2011", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string username;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:cdc:iisb:2011", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string password;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:cdc:iisb:2011", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string facilityID;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:cdc:iisb:2011", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string hl7Message;
        
        public submitSingleMessageRequest()
        {
        }
        
        public submitSingleMessageRequest(string username, string password, string facilityID, string hl7Message)
        {
            this.username = username;
            this.password = password;
            this.facilityID = facilityID;
            this.hl7Message = hl7Message;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="submitSingleMessageResponse", WrapperNamespace="urn:cdc:iisb:2011", IsWrapped=true)]
    public partial class submitSingleMessageResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:cdc:iisb:2011", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string @return;
        
        public submitSingleMessageResponse()
        {
        }
        
        public submitSingleMessageResponse(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface IIS_PortTypeChannel : Wi.Dpi.Immunizations.Dhs.Wir.IIS_PortType, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class IS_PortTypeClient : System.ServiceModel.ClientBase<Wi.Dpi.Immunizations.Dhs.Wir.IIS_PortType>, Wi.Dpi.Immunizations.Dhs.Wir.IIS_PortType
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public IS_PortTypeClient() : 
                base(IS_PortTypeClient.GetDefaultBinding(), IS_PortTypeClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.client_Port_Soap12.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IS_PortTypeClient(EndpointConfiguration endpointConfiguration) : 
                base(IS_PortTypeClient.GetBindingForEndpoint(endpointConfiguration), IS_PortTypeClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IS_PortTypeClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(IS_PortTypeClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IS_PortTypeClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(IS_PortTypeClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IS_PortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Wi.Dpi.Immunizations.Dhs.Wir.connectivityTestResponse> Wi.Dpi.Immunizations.Dhs.Wir.IIS_PortType.connectivityTestAsync(Wi.Dpi.Immunizations.Dhs.Wir.connectivityTestRequest request)
        {
            return base.Channel.connectivityTestAsync(request);
        }
        
        public System.Threading.Tasks.Task<Wi.Dpi.Immunizations.Dhs.Wir.connectivityTestResponse> connectivityTestAsync(string echoBack)
        {
            Wi.Dpi.Immunizations.Dhs.Wir.connectivityTestRequest inValue = new Wi.Dpi.Immunizations.Dhs.Wir.connectivityTestRequest();
            inValue.echoBack = echoBack;
            return ((Wi.Dpi.Immunizations.Dhs.Wir.IIS_PortType)(this)).connectivityTestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Wi.Dpi.Immunizations.Dhs.Wir.submitSingleMessageResponse> Wi.Dpi.Immunizations.Dhs.Wir.IIS_PortType.submitSingleMessageAsync(Wi.Dpi.Immunizations.Dhs.Wir.submitSingleMessageRequest request)
        {
            return base.Channel.submitSingleMessageAsync(request);
        }
        
        public System.Threading.Tasks.Task<Wi.Dpi.Immunizations.Dhs.Wir.submitSingleMessageResponse> submitSingleMessageAsync(string username, string password, string facilityID, string hl7Message)
        {
            Wi.Dpi.Immunizations.Dhs.Wir.submitSingleMessageRequest inValue = new Wi.Dpi.Immunizations.Dhs.Wir.submitSingleMessageRequest();
            inValue.username = username;
            inValue.password = password;
            inValue.facilityID = facilityID;
            inValue.hl7Message = hl7Message;
            return ((Wi.Dpi.Immunizations.Dhs.Wir.IIS_PortType)(this)).submitSingleMessageAsync(inValue);
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
            if ((endpointConfiguration == EndpointConfiguration.client_Port_Soap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.client_Port_Soap12))
            {
                return new System.ServiceModel.EndpointAddress("https://wir.dhswir.org/trn-webservices/cdc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return IS_PortTypeClient.GetBindingForEndpoint(EndpointConfiguration.client_Port_Soap12);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return IS_PortTypeClient.GetEndpointAddress(EndpointConfiguration.client_Port_Soap12);
        }
        
        public enum EndpointConfiguration
        {
            
            client_Port_Soap12,
        }
    }
}
