using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using System.Configuration;
using System.Threading;
//using PPIException;

namespace PPI.Core
{
    public enum ExceptionType
    {
        Fatal = 1,          //fatal, and exception handler does an explicit redirect
        NonFatal = 2,       //execution continues
        NonError = 3,       //for custom logging of specific events
        FatalNoRedirect = 4   //fatal, but mvc pipeline handles redirect, not exception handler
    }

    public class PPIEventData
    {
        public DateTime ExceptionDate;
        public ExceptionType ExceptionType;
        public bool? HasInnerExceptions;
        public string ExceptionMessage;
        public string ExceptionSource;  //The name of the application or the object that causes the error.  If not set, then the assembly
        public string ExceptionStackTrace;
        public string ExceptionFileName;
        public string ExceptionMethod;
        public string ExceptionLineNumber;
        public string ExceptionInnerExceptionData;
        public string RequestUserAgent;
        public bool? RequestHasCookies = null;
        public bool? RequestIsSecureConnection = null;
        public string RequestRawUrl;        //unmapped url
        public string RequestUrl;           //url shown in browser, might not be the actual page being displayed
        public string RequestQueryString;
        public string RequestHttpMethod;
        public bool? RequestIsCrawler = null;
        public string RequestPhysicalPath;
        public string RequestLocalIPAddress;
        public string RequestRemoteIPAddress;
        public string FormData;
        public string CookieData;
        public string SessionData;
    }

    public class ExceptionSvcHelper
    {
        public ExceptionSvcHelper()
        {

        }

        #region Helper Methods

        static private string[][] ToJaggedArray(Dictionary<string, string> collLookup)
        {
            string[][] arrLookup = new string[collLookup.Count][];
            int iIndex = 0;

            foreach (string sKey in collLookup.Keys)
            {
                arrLookup[iIndex++] = new string[] { sKey, collLookup[sKey] };
                //i++;
            }

            return (arrLookup);
        }

        static private CustomErrorCollection GetCustomErrors()
        {
            CustomErrorCollection collResult = null;

            try
            {
                Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/");

                if (objConfig != null)
                {
                    System.Web.Configuration.CustomErrorsSection objCustomErrors =
                        (CustomErrorsSection)objConfig.GetSection("system.web/customErrors");

                    if (objCustomErrors != null)
                    {
                        collResult = objCustomErrors.Errors;
                    }
                }
            }
            catch
            {
                collResult = null; ;
            }

            return (collResult);
        }

        static private string GetDefaultCustomErrorPage()
        {
            string sResult = string.Empty;

            try
            {
                Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/");

                if (objConfig != null)
                {
                    System.Web.Configuration.CustomErrorsSection objCustomErrors =
                        (CustomErrorsSection)objConfig.GetSection("system.web/customErrors");

                    if (objCustomErrors != null)
                    {
                        sResult = objCustomErrors.DefaultRedirect;
                    }
                }
            }
            catch
            {
                sResult = string.Empty;
            }

            return (sResult);
        }

        #endregion

        #region Display Methods

        static public void DumpExceptionInfo(PPIEventData objEventData)
        {
            HttpResponse objResponse = HttpContext.Current.Response;

            objResponse.Write("<br/><br/>EXCEPTION DATA:");
            objResponse.Write(String.Format("<br/>EventDate: {0}", objEventData.ExceptionDate));
            objResponse.Write(String.Format("<br/>EventType: {0}", objEventData.ExceptionType));
            objResponse.Write(String.Format("<br/>ExceptionMessage: {0}", objEventData.ExceptionMessage));
            objResponse.Write(String.Format("<br/>ExceptionMethod: {0}", objEventData.ExceptionMethod));
            objResponse.Write(String.Format("<br/>ExceptionSource: {0}", objEventData.ExceptionSource));
            objResponse.Write(String.Format("<br/>ExceptionStackTrace: {0}", objEventData.ExceptionStackTrace));
            objResponse.Write(String.Format("<br/>ExceptionFileName: {0}", objEventData.ExceptionFileName));
            objResponse.Write(String.Format("<br/>ExceptionLineNumber: {0}", objEventData.ExceptionLineNumber));
            objResponse.Write(String.Format("<br/>ExceptionInnerExceptionData:<br/> {0}",
                (objEventData.ExceptionInnerExceptionData ?? string.Empty).Replace(Environment.NewLine, "<br/>")));
        }

        static public void DumpRequestInfo(PPIEventData objEventData)
        {
            HttpResponse objResponse = HttpContext.Current.Response;

            objResponse.Write("<br/><br/>REQUEST DATA:");
            objResponse.Write(String.Format("<br/>User Agent: {0}", objEventData.RequestUserAgent));
            objResponse.Write(String.Format("<br/>Cookies Enabled?: {0}", objEventData.RequestHasCookies));
            objResponse.Write(String.Format("<br/>Is Secure? {0}", objEventData.RequestIsSecureConnection));
            objResponse.Write(String.Format("<br/>Is Crawler? {0}", objEventData.RequestIsCrawler));
            objResponse.Write(String.Format("<br/>Http Method: {0}", objEventData.RequestHttpMethod));
            objResponse.Write(String.Format("<br/>Local IP: {0}", objEventData.RequestLocalIPAddress));
            objResponse.Write(String.Format("<br/>Remote IP: {0}", objEventData.RequestRemoteIPAddress));
            objResponse.Write(String.Format("<br/>Raw Url: {0}", objEventData.RequestRawUrl));
            objResponse.Write(String.Format("<br/>Url: {0}", objEventData.RequestUrl));
            objResponse.Write(String.Format("<br/>Query String: {0}", objEventData.RequestQueryString));
            objResponse.Write(String.Format("<br/>Physical Path: {0}", objEventData.RequestPhysicalPath));
        }

        static public void DumpFormInfo(PPIEventData objEventData)
        {
            HttpResponse objResponse = HttpContext.Current.Response;

            objResponse.Write("<br/><br/>FORM DATA: <br/>");
            objResponse.Write(String.Format("{0}", (objEventData.FormData ?? string.Empty).Replace(Environment.NewLine, "<br/>")));
        }

        static public void DumpCookieInfo(PPIEventData objEventData)
        {
            HttpResponse objResponse = HttpContext.Current.Response;

            objResponse.Write("<br/><br/>COOKIE DATA: <br/>");
            objResponse.Write(String.Format("{0}", (objEventData.CookieData ?? string.Empty).Replace(Environment.NewLine, "<br/>")));
        }

        static public void DumpSessionInfo(PPIEventData objEventData)
        {
            HttpResponse objResponse = HttpContext.Current.Response;

            objResponse.Write("<br/><br/>SESSION DATA: <br/>");
            objResponse.Write(String.Format("{0}", (objEventData.SessionData ?? string.Empty).Replace(Environment.NewLine, "<br/>")));
        }

        #endregion

        #region Methods to Gather Data

        static private void GatherSessionInfo(PPIEventData objEventData)
        {
            StringBuilder sSessionData = new StringBuilder(string.Empty);            

            try
            {
                if (HttpContext.Current != null)
                {
                    HttpRequest objRequest = HttpContext.Current.Request;
                    HttpSessionState objSession = HttpContext.Current.Session;

                    if (objSession != null)
                    {
                        sSessionData.AppendFormat("SESSION ID: {0}", objSession.SessionID);
                        sSessionData.AppendLine();

                        for (int iSessionCount = 0; iSessionCount < objSession.Count; iSessionCount++)
                        {
                            sSessionData.AppendFormat("SESSION ITEM: {0}, ", objSession.Keys[iSessionCount]);

                            if (objSession[iSessionCount] != null)
                            {
                                if (objSession[iSessionCount].GetType().IsValueType)
                                {
                                    sSessionData.AppendFormat("SESSION VALUE: {0}", objSession.Keys[iSessionCount]);
                                }
                                else
                                {
                                    try
                                    {
                                        sSessionData.AppendFormat("SESSION VALUE: {0}", objSession[iSessionCount]);
                                    }
                                    catch
                                    {
                                        sSessionData.AppendFormat("SESSION VALUE TYPE: {0}", objSession[iSessionCount].GetType().Name);
                                    }
                                }
                            }
                            else
                            {
                                sSessionData.AppendFormat("SESSION VALUE: NULL");
                            }

                            sSessionData.AppendLine();
                        }
                    }

                    objEventData.SessionData = sSessionData.ToString();
                }
            }
            catch (Exception objExx)
            {
                Debug.WriteLine(objExx.Message);

                //throw objExx;   //rethrow
            }
        }

        static private void GatherFormInfo(PPIEventData objEventData)
        {
            try
            {
                StringBuilder sFormData = new StringBuilder(string.Empty);
                HttpRequest objRequest = HttpContext.Current.Request;

                if (objRequest.Form != null)
                {
                    if (objRequest.Form.AllKeys != null)
                    {
                        foreach (string sKey in objRequest.Form.AllKeys)
                        {
                            //Don't capture viewstate
                            //test in .NET 4
                            if (sKey.Length < 3 || sKey.Substring(0, 2) != "__")
                            {
                                sFormData.AppendFormat("FORM ITEM: {0}, FORM VALUE: {1}", sKey, objRequest.Form[sKey]);
                                sFormData.AppendLine();
                            }
                        }
                    }
                }

                objEventData.FormData = sFormData.ToString();
            }
            catch (Exception objExx)
            {
                Debug.WriteLine(objExx.Message);

                //throw objExx;   //rethrow                
            }
        }

        static private void GatherCookieInfo(PPIEventData objEventData)
        {
            try
            {
                StringBuilder sCookieData = new StringBuilder(string.Empty);
                HttpRequest objRequest = HttpContext.Current.Request;

                if (objRequest.Cookies != null)
                {
                    for (int iCookieCount = 0; iCookieCount < objRequest.Cookies.Count; iCookieCount++)
                    {
                        HttpCookie objCookie = objRequest.Cookies[iCookieCount];

                        sCookieData.AppendFormat("COOKIE NAME: {0}, COOKIE DOMAIN: {1}, COOKIE EXPIRATION: {2}, COOKIE PATH: {3}, COOKIE SECURE: {4}",
                            objCookie.Name, objCookie.Domain, objCookie.Expires, objCookie.Path, objCookie.Secure);

                        if (!objCookie.HasKeys)
                        {
                            sCookieData.AppendFormat(", COOKIE VALUE: {0}", objCookie.Value);
                        }
                        else    //multi valued
                        {
                            sCookieData.AppendLine("VALUES");

                            for (int iValueCount = 0; iValueCount < objCookie.Values.Count; iValueCount++)
                            {
                                sCookieData.AppendFormat(", COOKIE KEY#{0}: {1}, COOKIE VALUE#{2}: {3}",
                                    iValueCount + 1, objCookie.Values.AllKeys[iValueCount], iValueCount + 1, objCookie.Values[iValueCount]);
                            }
                        }

                        sCookieData.AppendLine();
                    }
                }

                objEventData.CookieData = sCookieData.ToString();
            }
            catch (Exception objExx)
            {
                Debug.WriteLine(objExx.Message);

                //throw objExx;   //rethrow             
            }
        }

        static private void GatherRequestInfo(PPIEventData objEventData)
        {
            try
            {
                HttpRequest objRequest = HttpContext.Current.Request;

                if (objRequest != null)
                {
                    objEventData.RequestUserAgent = objRequest.UserAgent;   //unparsed user agent
                    objEventData.RequestHasCookies = objRequest.Browser.Cookies;
                    objEventData.RequestIsSecureConnection = objRequest.IsSecureConnection;
                    objEventData.RequestRawUrl = objRequest.RawUrl;
                    objEventData.RequestHttpMethod = objRequest.HttpMethod;
                    objEventData.RequestPhysicalPath = objRequest.PhysicalPath;

                    if (objRequest.ServerVariables != null && objRequest.ServerVariables.Count > 0)
                    {
                        objEventData.RequestQueryString = objRequest.ServerVariables["QUERY_STRING"];
                        objEventData.RequestLocalIPAddress = objRequest.ServerVariables["LOCAL_ADDR"];
                        objEventData.RequestRemoteIPAddress = objRequest.ServerVariables["REMOTE_ADDR"];
                    }

                    if (objRequest.Url != null)
                    {
                        objEventData.RequestUrl = objRequest.Url.AbsoluteUri;
                    }

                    if (objRequest.Browser != null)
                    {
                        objEventData.RequestIsCrawler = objRequest.Browser.Crawler;
                    }
                }

            }
            catch (Exception objExx)
            {
                Debug.WriteLine(objExx.Message);

               // throw objExx;   //rethrow
            }
        }

        static private void GatherExceptionInfo(Exception objEx, PPIEventData objEventData, bool bIsInner)
        {
            try
            {
                if (objEx != null)
                {
                    objEventData.ExceptionMessage = objEx.Message;
                    objEventData.ExceptionSource = objEx.Source;
                    objEventData.HasInnerExceptions = (objEx.InnerException != null);

                    StackTrace objStackTrace = new StackTrace(objEx, 0, true);

                    if (objStackTrace != null) //&& !isInner)  //only want stack trace for parent exception
                    {
                        if (!bIsInner)
                        {
                            objEventData.ExceptionStackTrace = objStackTrace.ToString();
                        }

                        StackFrame objStackFrame = objStackTrace.GetFrame(0);

                        if (objStackFrame != null)
                        {
                            MethodBase objMethodBase;

                            objEventData.ExceptionFileName = objStackFrame.GetFileName();
                            objEventData.ExceptionLineNumber = objStackFrame.GetFileLineNumber().ToString();

                            objMethodBase = objStackFrame.GetMethod();

                            if (objMethodBase != null)
                            {
                                objEventData.ExceptionMethod = objMethodBase.Name;
                            }
                        }
                    }

                    if (objEx.InnerException != null && !bIsInner)
                    {
                        int iInnerCount = 0;
                        ExceptionSvcProxy objExSvc = new ExceptionSvcProxy();
                        int maxInnerExceptions = objExSvc.MaxInnerExceptions();

                        while (objEx.InnerException != null && iInnerCount < maxInnerExceptions)
                        {
                            iInnerCount++;
                            PPIEventData objInnerData = new PPIEventData();
                            GatherExceptionInfo(objEx.InnerException, objInnerData, true);
                            objEventData.ExceptionInnerExceptionData +=
                                string.Format("INNER EXCEPTION #{0}: MSG: {1}, SOURCE: {2}, FILE: {3}, METHOD: {4}, LINE: {5}{6}",
                                    iInnerCount,
                                    objInnerData.ExceptionMessage,
                                    objInnerData.ExceptionSource,
                                    objInnerData.ExceptionFileName,
                                    objInnerData.ExceptionMethod,
                                    objInnerData.ExceptionLineNumber,
                                    Environment.NewLine);

                            objEx = objEx.InnerException;
                        }
                    }
                }
            }
            catch (Exception objExx)
            {
                Debug.WriteLine(objExx.Message);

                throw objExx;   //rethrow                
            }
        }

        #endregion

        [Log]
        static public string LogEvent(string sMessage)
        {
            return (HandleException(sMessage, null, ExceptionType.NonError));
        }

        //called from global.asax or other global handler.  always fatal
        [Log]
        static public void HandleUnhandledException(Exception objEx)
        {
            //For debugging - if customErrors if OFF then no error logging, just rethrow and see yellow screen of death
            if (!HttpContext.Current.IsCustomErrorEnabled)
            {
                if (objEx != null)
                {
                    throw objEx;
                }
            }
            else
            {
                HandleException(objEx, ExceptionType.Fatal);    //don't care about return value            
            }
        }

        //[Log]
        //static public string HandleException(Exception objEx)
        //{
        //    return (HandleException(objEx.Message, objEx, ExceptionType.NonFatal));
        //}

        [Log]
        static public string HandleException(Exception objEx, ExceptionType eExType)
        {
            return (HandleException(objEx.Message, objEx, eExType));
        }

        [Log]
        static public string HandleNonFatalException(Exception objEx)
        {
            return (HandleException(objEx.Message, objEx, ExceptionType.NonFatal));
        }

        [Log]
        static public string HandleFatalException(Exception objEx)
        {
            return (HandleException(objEx.Message, objEx, ExceptionType.Fatal));
        }

        [Log]
        static public string HandleFatalExceptionNoRedirect(Exception objEx)
        {
            return (HandleException(objEx.Message, objEx, ExceptionType.FatalNoRedirect));
        }
        
        static private string HandleException(string sMessage, Exception objEx, ExceptionType eExType)
        {
            string sResult = string.Empty;
            string sErrorPage = string.Empty;
            PPIEventData objEventData = new PPIEventData();
            Dictionary<string, string> collExceptionInfo = null;
            Dictionary<string, string> collRequestInfo = null;
            objEventData.ExceptionDate = DateTime.Now;
            objEventData.ExceptionType = eExType;
            ExceptionSvcProxy objExSvc = new ExceptionSvcProxy();

            try
            {
                AuthenticationHeader objAuth = new AuthenticationHeader();
                objAuth.UserName = ConfigurationManager.AppSettings["xuser"];
                objAuth.Password = ConfigurationManager.AppSettings["xpwd"];                

                objExSvc.AuthenticationHeaderValue = objAuth;

                if (eExType == ExceptionType.NonError)
                {
                    //logging
                    string sLogResult = objExSvc.LogEvent((int)objEventData.ExceptionType,
                            objEventData.ExceptionDate,
                            ConfigurationManager.AppSettings["senderApp"],
                            sMessage,
                            null,
                            null,
                            null,
                            null,
                            null);

                    Debug.WriteLine(string.Format("Exception Helper (E1): {0}", sLogResult));

                    if (sLogResult != string.Empty)
                    {
                        sResult = sLogResult;
                    }
                }
                else
                {
                    collExceptionInfo = new Dictionary<string, string>();
                    collRequestInfo = new Dictionary<string, string>();

                    //exception
                    GatherExceptionInfo(objEx, objEventData, false);
                    collExceptionInfo.Add("EXSOURCE", objEventData.ExceptionSource);
                    collExceptionInfo.Add("EXTRACE", objEventData.ExceptionStackTrace);
                    collExceptionInfo.Add("EXFILE", objEventData.ExceptionFileName);
                    collExceptionInfo.Add("EXMETH", objEventData.ExceptionMethod);
                    collExceptionInfo.Add("EXLINE", objEventData.ExceptionLineNumber);
                    collExceptionInfo.Add("EXINNER", objEventData.ExceptionInnerExceptionData);

                    //request
                    GatherRequestInfo(objEventData);
                    collRequestInfo.Add("REQUSERAGENT", objEventData.RequestUserAgent);
                    collRequestInfo.Add("REQCOOKIESYN", (objEventData.RequestHasCookies != null ? objEventData.RequestHasCookies.ToString() : string.Empty));
                    collRequestInfo.Add("REQSECUREYN", (objEventData.RequestIsSecureConnection != null ? objEventData.RequestIsSecureConnection.ToString() : string.Empty));
                    collRequestInfo.Add("REQCRAWLERYN", (objEventData.RequestIsCrawler != null ? objEventData.RequestIsCrawler.ToString() : string.Empty));
                    collRequestInfo.Add("REQMETHOD", objEventData.RequestHttpMethod);
                    collRequestInfo.Add("REQLOCALIP", objEventData.RequestLocalIPAddress);
                    collRequestInfo.Add("REQREMOTEIP", objEventData.RequestRemoteIPAddress);
                    collRequestInfo.Add("REQRAWURL", objEventData.RequestRawUrl);
                    collRequestInfo.Add("REQURL", objEventData.RequestUrl);
                    collRequestInfo.Add("REQQUERYSTRING", objEventData.RequestQueryString);
                    collRequestInfo.Add("REQPATH", objEventData.RequestPhysicalPath);

                    //form
                    GatherFormInfo(objEventData);

                    //cookies
                    GatherCookieInfo(objEventData);

                    //session
                    GatherSessionInfo(objEventData);

                    string[][] arrExceptionInfo = ToJaggedArray(collExceptionInfo);
                    string[][] arrRequestInfo = ToJaggedArray(collRequestInfo);

                    string sLogResult = objExSvc.LogEvent((int)objEventData.ExceptionType,
                        objEventData.ExceptionDate,
                        ConfigurationManager.AppSettings["senderApp"],
                        objEventData.ExceptionMessage,
                        arrExceptionInfo,
                        arrRequestInfo,
                        objEventData.FormData,
                        objEventData.CookieData,
                        objEventData.SessionData);

                    Debug.WriteLine(string.Format("Exception Helper (E2): {0}", sLogResult));

                    if (sLogResult != string.Empty)
                    {
                        sResult = sLogResult;
                    }

                    //DEBUGGING
                    //DumpExceptionInfo(objEventData);
                    //DumpRequestInfo(objEventData);
                    //DumpFormInfo(objEventData);
                    //DumpCookieInfo(objEventData);
                    //DumpSessionInfo(objEventData);                                                
                }

                //done logging, handle fatal error redirect.
                //redirect whether logging succeeded or not
                if (eExType == ExceptionType.Fatal)
                {
                    //determine error handling page - either the default, or specific http error page                    

                    //check if http error then see if customerrors section has a specific page to go to
                    if (objEx.GetType() == typeof(HttpException))
                    {
                        HttpException objHttpEx = (HttpException)objEx;
                        CustomErrorCollection collCustomErrors = GetCustomErrors();
                        string sHttpCode = objHttpEx.GetHttpCode().ToString();

                        if ((collCustomErrors[sHttpCode].Redirect ?? string.Empty) != string.Empty)
                        {
                            sErrorPage = collCustomErrors[sHttpCode].Redirect;
                        }
                        else
                        {
                            sErrorPage = GetDefaultCustomErrorPage();
                        }
                    }
                    else
                    {
                        sErrorPage = GetDefaultCustomErrorPage();
                    }

                    if (sErrorPage != string.Empty)
                    {
                        //HttpContext.Current.Server.Transfer(sErrorPage, true);        //Leave
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.Redirect(sErrorPage, false);             //Leave

                        //HttpContext.Current.Response.Redirect("~/Error404");             //Leave
                    }
                }

                return (sResult);
            }
            catch (ThreadAbortException)
            {
                //do nothing since server.transfer will trigger this error
            }
            catch (Exception objExx)
            {
                Debug.WriteLine(string.Format("Exception Helper ERROR (E3): {0}", objExx.Message));

                //if an error occurs during error handling, still try to redirect                
                if (eExType == ExceptionType.Fatal && sErrorPage != string.Empty)
                {
                    try
                    {
                        HttpContext.Current.Server.Transfer(sErrorPage, true);        //Leave
                    }
                    catch (ThreadAbortException)
                    {
                        //do nothing since server.transfer will trigger this error
                    }
                }

                //don't rethrow, don't want infinite loop of error handling
            }
            finally
            {

            }

            return (string.Empty);
        }
    }
}

