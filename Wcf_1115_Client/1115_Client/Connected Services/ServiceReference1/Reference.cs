﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _1115_Client.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Image_Data", Namespace="http://schemas.datacontract.org/2004/07/ConsoleApp_1115_%EC%A1%B0%EB%B3%84%EC%8B%" +
        "A4%EC%8A%B5_%EC%96%BC%EA%B5%B4_%EC%B6%94%EC%B6%9C_%ED%94%84%EB%A1%9C%EA%B7%B8%EB" +
        "%9E%A8_")]
    [System.SerializableAttribute()]
    public partial class Image_Data : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] Analyzed_imageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Image_NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private _1115_Client.ServiceReference1.Image_Each_Person[] imageListsField;
        
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
        public byte[] Analyzed_image {
            get {
                return this.Analyzed_imageField;
            }
            set {
                if ((object.ReferenceEquals(this.Analyzed_imageField, value) != true)) {
                    this.Analyzed_imageField = value;
                    this.RaisePropertyChanged("Analyzed_image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Image_Name {
            get {
                return this.Image_NameField;
            }
            set {
                if ((object.ReferenceEquals(this.Image_NameField, value) != true)) {
                    this.Image_NameField = value;
                    this.RaisePropertyChanged("Image_Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public _1115_Client.ServiceReference1.Image_Each_Person[] imageLists {
            get {
                return this.imageListsField;
            }
            set {
                if ((object.ReferenceEquals(this.imageListsField, value) != true)) {
                    this.imageListsField = value;
                    this.RaisePropertyChanged("imageLists");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Image_Each_Person", Namespace="http://schemas.datacontract.org/2004/07/ConsoleApp_1115_%EC%A1%B0%EB%B3%84%EC%8B%" +
        "A4%EC%8A%B5_%EC%96%BC%EA%B5%B4_%EC%B6%94%EC%B6%9C_%ED%94%84%EB%A1%9C%EA%B7%B8%EB" +
        "%9E%A8_")]
    [System.SerializableAttribute()]
    public partial class Image_Each_Person : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] Each_Single_ImageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Expectancy_ageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Image_NameField;
        
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
        public byte[] Each_Single_Image {
            get {
                return this.Each_Single_ImageField;
            }
            set {
                if ((object.ReferenceEquals(this.Each_Single_ImageField, value) != true)) {
                    this.Each_Single_ImageField = value;
                    this.RaisePropertyChanged("Each_Single_Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Expectancy_age {
            get {
                return this.Expectancy_ageField;
            }
            set {
                if ((object.ReferenceEquals(this.Expectancy_ageField, value) != true)) {
                    this.Expectancy_ageField = value;
                    this.RaisePropertyChanged("Expectancy_age");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Gender {
            get {
                return this.GenderField;
            }
            set {
                if ((object.ReferenceEquals(this.GenderField, value) != true)) {
                    this.GenderField = value;
                    this.RaisePropertyChanged("Gender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Image_Name {
            get {
                return this.Image_NameField;
            }
            set {
                if ((object.ReferenceEquals(this.Image_NameField, value) != true)) {
                    this.Image_NameField = value;
                    this.RaisePropertyChanged("Image_Name");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IFace", CallbackContract=typeof(_1115_Client.ServiceReference1.IFaceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IFace {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFace/Image_Send")]
        void Image_Send(string strFileName, byte[] bytePicture);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFace/Image_Send")]
        System.Threading.Tasks.Task Image_SendAsync(string strFileName, byte[] bytePicture);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsInitiating=false, Action="http://tempuri.org/IFace/Image_Delete")]
        void Image_Delete(string strFileName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsInitiating=false, Action="http://tempuri.org/IFace/Image_Delete")]
        System.Threading.Tasks.Task Image_DeleteAsync(string strFileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFace/Init_Send_Image_Data", ReplyAction="http://tempuri.org/IFace/Init_Send_Image_DataResponse")]
        _1115_Client.ServiceReference1.Image_Data[] Init_Send_Image_Data();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFace/Init_Send_Image_Data", ReplyAction="http://tempuri.org/IFace/Init_Send_Image_DataResponse")]
        System.Threading.Tasks.Task<_1115_Client.ServiceReference1.Image_Data[]> Init_Send_Image_DataAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFaceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFace/Image_Send_Result_Single")]
        void Image_Send_Result_Single(_1115_Client.ServiceReference1.Image_Data ImageData, string msg);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFace/Image_Send_Result")]
        void Image_Send_Result(_1115_Client.ServiceReference1.Image_Data[] Image_List);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFace/Image_Delete_Result")]
        void Image_Delete_Result(_1115_Client.ServiceReference1.Image_Data[] Image_List);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFaceChannel : _1115_Client.ServiceReference1.IFace, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FaceClient : System.ServiceModel.DuplexClientBase<_1115_Client.ServiceReference1.IFace>, _1115_Client.ServiceReference1.IFace {
        
        public FaceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public FaceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public FaceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FaceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FaceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Image_Send(string strFileName, byte[] bytePicture) {
            base.Channel.Image_Send(strFileName, bytePicture);
        }
        
        public System.Threading.Tasks.Task Image_SendAsync(string strFileName, byte[] bytePicture) {
            return base.Channel.Image_SendAsync(strFileName, bytePicture);
        }
        
        public void Image_Delete(string strFileName) {
            base.Channel.Image_Delete(strFileName);
        }
        
        public System.Threading.Tasks.Task Image_DeleteAsync(string strFileName) {
            return base.Channel.Image_DeleteAsync(strFileName);
        }
        
        public _1115_Client.ServiceReference1.Image_Data[] Init_Send_Image_Data() {
            return base.Channel.Init_Send_Image_Data();
        }
        
        public System.Threading.Tasks.Task<_1115_Client.ServiceReference1.Image_Data[]> Init_Send_Image_DataAsync() {
            return base.Channel.Init_Send_Image_DataAsync();
        }
    }
}
