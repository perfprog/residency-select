﻿//using System;
//using System.Collections.Generic;
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=4.0.30319.18020.
// 
namespace PPI.Core
{
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "PPIExceptionSvcSoap", Namespace = "http://tempuri.org/")]
    public partial class ExceptionSvcProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback TestOperationCompleted;

        private System.Threading.SendOrPostCallback MaxInnerExceptionsOperationCompleted;

        private AuthenticationHeader authenticationHeaderValueField;

        private System.Threading.SendOrPostCallback LogEventOperationCompleted;

        /// <remarks/>
        public ExceptionSvcProxy()
        {
            string urlSetting = System.Configuration.ConfigurationManager.AppSettings["exceptionSvcUrl"];
            if ((urlSetting != null))
            {
                this.Url = urlSetting;
            }
            else
            {
                this.Url = "http://exception.perfprog.com/ppiexceptionsvc.asmx";
            }
        }

        public AuthenticationHeader AuthenticationHeaderValue
        {
            get
            {
                return this.authenticationHeaderValueField;
            }
            set
            {
                this.authenticationHeaderValueField = value;
            }
        }

        /// <remarks/>
        public event TestCompletedEventHandler TestCompleted;

        /// <remarks/>
        public event MaxInnerExceptionsCompletedEventHandler MaxInnerExceptionsCompleted;

        /// <remarks/>
        public event LogEventCompletedEventHandler LogEventCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Test", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Test()
        {
            object[] results = this.Invoke("Test", new object[0]);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginTest(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Test", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string EndTest(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void TestAsync()
        {
            this.TestAsync(null);
        }

        /// <remarks/>
        public void TestAsync(object userState)
        {
            if ((this.TestOperationCompleted == null))
            {
                this.TestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestOperationCompleted);
            }
            this.InvokeAsync("Test", new object[0], this.TestOperationCompleted, userState);
        }

        private void OnTestOperationCompleted(object arg)
        {
            if ((this.TestCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestCompleted(this, new TestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/MaxInnerExceptions", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int MaxInnerExceptions()
        {
            object[] results = this.Invoke("MaxInnerExceptions", new object[0]);
            return ((int)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginMaxInnerExceptions(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("MaxInnerExceptions", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public int EndMaxInnerExceptions(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void MaxInnerExceptionsAsync()
        {
            this.MaxInnerExceptionsAsync(null);
        }

        /// <remarks/>
        public void MaxInnerExceptionsAsync(object userState)
        {
            if ((this.MaxInnerExceptionsOperationCompleted == null))
            {
                this.MaxInnerExceptionsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnMaxInnerExceptionsOperationCompleted);
            }
            this.InvokeAsync("MaxInnerExceptions", new object[0], this.MaxInnerExceptionsOperationCompleted, userState);
        }

        private void OnMaxInnerExceptionsOperationCompleted(object arg)
        {
            if ((this.MaxInnerExceptionsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.MaxInnerExceptionsCompleted(this, new MaxInnerExceptionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenticationHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/LogEvent", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string LogEvent(int iEventType, System.DateTime dtEventDate, string sSenderApp, string sMessage, [System.Xml.Serialization.XmlArrayItemAttribute("ArrayOfString")] [System.Xml.Serialization.XmlArrayItemAttribute(NestingLevel = 1)] string[][] arrExceptionInfo, [System.Xml.Serialization.XmlArrayItemAttribute("ArrayOfString")] [System.Xml.Serialization.XmlArrayItemAttribute(NestingLevel = 1)] string[][] arrRequestInfo, string sFormData, string sCookieData, string sSessionData)
        {
            object[] results = this.Invoke("LogEvent", new object[] {
                        iEventType,
                        dtEventDate,
                        sSenderApp,
                        sMessage,
                        arrExceptionInfo,
                        arrRequestInfo,
                        sFormData,
                        sCookieData,
                        sSessionData});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginLogEvent(int iEventType, System.DateTime dtEventDate, string sSenderApp, string sMessage, string[][] arrExceptionInfo, string[][] arrRequestInfo, string sFormData, string sCookieData, string sSessionData, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("LogEvent", new object[] {
                        iEventType,
                        dtEventDate,
                        sSenderApp,
                        sMessage,
                        arrExceptionInfo,
                        arrRequestInfo,
                        sFormData,
                        sCookieData,
                        sSessionData}, callback, asyncState);
        }

        /// <remarks/>
        public string EndLogEvent(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void LogEventAsync(int iEventType, System.DateTime dtEventDate, string sSenderApp, string sMessage, string[][] arrExceptionInfo, string[][] arrRequestInfo, string sFormData, string sCookieData, string sSessionData)
        {
            this.LogEventAsync(iEventType, dtEventDate, sSenderApp, sMessage, arrExceptionInfo, arrRequestInfo, sFormData, sCookieData, sSessionData, null);
        }

        /// <remarks/>
        public void LogEventAsync(int iEventType, System.DateTime dtEventDate, string sSenderApp, string sMessage, string[][] arrExceptionInfo, string[][] arrRequestInfo, string sFormData, string sCookieData, string sSessionData, object userState)
        {
            if ((this.LogEventOperationCompleted == null))
            {
                this.LogEventOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogEventOperationCompleted);
            }
            this.InvokeAsync("LogEvent", new object[] {
                        iEventType,
                        dtEventDate,
                        sSenderApp,
                        sMessage,
                        arrExceptionInfo,
                        arrRequestInfo,
                        sFormData,
                        sCookieData,
                        sSessionData}, this.LogEventOperationCompleted, userState);
        }

        private void OnLogEventOperationCompleted(object arg)
        {
            if ((this.LogEventCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogEventCompleted(this, new LogEventCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
    public partial class AuthenticationHeader : System.Web.Services.Protocols.SoapHeader
    {

        private string userNameField;

        private string passwordField;

        private System.Xml.XmlAttribute[] anyAttrField;

        /// <remarks/>
        public string UserName
        {
            get
            {
                return this.userNameField;
            }
            set
            {
                this.userNameField = value;
            }
        }

        /// <remarks/>
        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr
        {
            get
            {
                return this.anyAttrField;
            }
            set
            {
                this.anyAttrField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void TestCompletedEventHandler(object sender, TestCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal TestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void MaxInnerExceptionsCompletedEventHandler(object sender, MaxInnerExceptionsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MaxInnerExceptionsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal MaxInnerExceptionsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void LogEventCompletedEventHandler(object sender, LogEventCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LogEventCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal LogEventCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}
