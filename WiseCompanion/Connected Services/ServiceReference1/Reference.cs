﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IKentapAFE")]
    public interface IKentapAFE
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/FindMyCar", ReplyAction="http://tempuri.org/IKentapAFE/FindMyCarResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> FindMyCarAsync(string emailAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/GetSettings", ReplyAction="http://tempuri.org/IKentapAFE/GetSettingsResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetSettingsAsync(string emailAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/GetMedication", ReplyAction="http://tempuri.org/IKentapAFE/GetMedicationResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetMedicationAsync(string emailAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/GetMedicationList", ReplyAction="http://tempuri.org/IKentapAFE/GetMedicationListResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetMedicationListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/GetSOS", ReplyAction="http://tempuri.org/IKentapAFE/GetSOSResponse")]
        System.Threading.Tasks.Task<string> GetSOSAsync(string emailAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/Login", ReplyAction="http://tempuri.org/IKentapAFE/LoginResponse")]
        System.Threading.Tasks.Task<bool> LoginAsync(string emailAddress, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/SaveSettings", ReplyAction="http://tempuri.org/IKentapAFE/SaveSettingsResponse")]
        System.Threading.Tasks.Task<bool> SaveSettingsAsync(string emailAddress, string FontFamily, int FontSize, string BackgroundColour);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/DeleteMedication", ReplyAction="http://tempuri.org/IKentapAFE/DeleteMedicationResponse")]
        System.Threading.Tasks.Task<bool> DeleteMedicationAsync(string emailAddress, string MedicationDescription, int Dosage, string Time);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/AddMedication", ReplyAction="http://tempuri.org/IKentapAFE/AddMedicationResponse")]
        System.Threading.Tasks.Task<bool> AddMedicationAsync(string emailAddress, string MedicationDescription, int Dosage, string Time);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/SignUp", ReplyAction="http://tempuri.org/IKentapAFE/SignUpResponse")]
        System.Threading.Tasks.Task<string> SignUpAsync(string emailAddress, string password, string name, string sosPhoneNumber, string adminEmail, string adminPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/IsThereACurrentActiveLocation", ReplyAction="http://tempuri.org/IKentapAFE/IsThereACurrentActiveLocationResponse")]
        System.Threading.Tasks.Task<bool> IsThereACurrentActiveLocationAsync(string emailAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/SaveCurrentLocation", ReplyAction="http://tempuri.org/IKentapAFE/SaveCurrentLocationResponse")]
        System.Threading.Tasks.Task<bool> SaveCurrentLocationAsync(string emailAddress, string Longitude, string Latitude);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKentapAFE/RemoveCurrentLocation", ReplyAction="http://tempuri.org/IKentapAFE/RemoveCurrentLocationResponse")]
        System.Threading.Tasks.Task<bool> RemoveCurrentLocationAsync(string emailAddress);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public interface IKentapAFEChannel : ServiceReference1.IKentapAFE, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public partial class KentapAFEClient : System.ServiceModel.ClientBase<ServiceReference1.IKentapAFE>, ServiceReference1.IKentapAFE
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public KentapAFEClient(EndpointConfiguration endpointConfiguration) : 
                base(KentapAFEClient.GetBindingForEndpoint(endpointConfiguration), KentapAFEClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public KentapAFEClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(KentapAFEClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public KentapAFEClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(KentapAFEClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public KentapAFEClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> FindMyCarAsync(string emailAddress)
        {
            return base.Channel.FindMyCarAsync(emailAddress);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetSettingsAsync(string emailAddress)
        {
            return base.Channel.GetSettingsAsync(emailAddress);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetMedicationAsync(string emailAddress)
        {
            return base.Channel.GetMedicationAsync(emailAddress);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetMedicationListAsync()
        {
            return base.Channel.GetMedicationListAsync();
        }
        
        public System.Threading.Tasks.Task<string> GetSOSAsync(string emailAddress)
        {
            return base.Channel.GetSOSAsync(emailAddress);
        }
        
        public System.Threading.Tasks.Task<bool> LoginAsync(string emailAddress, string password)
        {
            return base.Channel.LoginAsync(emailAddress, password);
        }
        
        public System.Threading.Tasks.Task<bool> SaveSettingsAsync(string emailAddress, string FontFamily, int FontSize, string BackgroundColour)
        {
            return base.Channel.SaveSettingsAsync(emailAddress, FontFamily, FontSize, BackgroundColour);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteMedicationAsync(string emailAddress, string MedicationDescription, int Dosage, string Time)
        {
            return base.Channel.DeleteMedicationAsync(emailAddress, MedicationDescription, Dosage, Time);
        }
        
        public System.Threading.Tasks.Task<bool> AddMedicationAsync(string emailAddress, string MedicationDescription, int Dosage, string Time)
        {
            return base.Channel.AddMedicationAsync(emailAddress, MedicationDescription, Dosage, Time);
        }
        
        public System.Threading.Tasks.Task<string> SignUpAsync(string emailAddress, string password, string name, string sosPhoneNumber, string adminEmail, string adminPassword)
        {
            return base.Channel.SignUpAsync(emailAddress, password, name, sosPhoneNumber, adminEmail, adminPassword);
        }
        
        public System.Threading.Tasks.Task<bool> IsThereACurrentActiveLocationAsync(string emailAddress)
        {
            return base.Channel.IsThereACurrentActiveLocationAsync(emailAddress);
        }
        
        public System.Threading.Tasks.Task<bool> SaveCurrentLocationAsync(string emailAddress, string Longitude, string Latitude)
        {
            return base.Channel.SaveCurrentLocationAsync(emailAddress, Longitude, Latitude);
        }
        
        public System.Threading.Tasks.Task<bool> RemoveCurrentLocationAsync(string emailAddress)
        {
            return base.Channel.RemoveCurrentLocationAsync(emailAddress);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IKentapAFE))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpsBinding_IKentapAFE))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IKentapAFE))
            {
                return new System.ServiceModel.EndpointAddress("http://kentapafe20240223.azurewebsites.net/KentapAFE.svc");
            }
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpsBinding_IKentapAFE))
            {
                return new System.ServiceModel.EndpointAddress("https://kentapafe20240223.azurewebsites.net/KentapAFE.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IKentapAFE,
            
            BasicHttpsBinding_IKentapAFE,
        }
    }
}
