using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Xml;
using System.IO;
using System.Configuration;


using System.Net.Mail;
using System.Security.Cryptography;

namespace PPI.Core.Web.Infrastructure
{
    public class MailClass
    {
        #region Email

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        public static void SendEmail(string subject, string body, string from, string to)
        {
            SendEmail(subject, body, new MailAddress(from), new MailAddress(to),
                new List<String>(), new List<String>(), true);
            
        }

     

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="to"></param>
        public static bool SendEmail(string subject, string body, string to)
        {
           return SendEmail(subject, body, null, new MailAddress(to), new List<String>(), new List<String>(), true);
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="to"></param>
        public static bool SendEmail(string subject, string body, string to, bool isHtml)
        {
           return SendEmail(subject, body, null, new MailAddress(to), new List<String>(), new List<String>(), isHtml);
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        /// <param name="bcc">BCC</param>
        /// <param name="cc">CC</param>
        /// <param name="emailAccount">Email account to use</param>
        public static bool SendEmail(string subject, string body,
            MailAddress from, MailAddress to, List<string> bcc, List<string> cc, bool isHtml)
        {
            var message = new MailMessage();
            bool isSuccess = false;
            try
            {
                if (from != null)
                    message.From = from;

                message.To.Add(to);
                if (null != bcc)
                    foreach (string address in bcc)
                    {
                        if (address != null)
                        {
                            if (!String.IsNullOrEmpty(address.Trim()))
                            {
                                message.Bcc.Add(address.Trim());
                            }
                        }
                    }
                if (null != cc)
                    foreach (string address in cc)
                    {
                        if (address != null)
                        {
                            if (!String.IsNullOrEmpty(address.Trim()))
                            {
                                message.CC.Add(address.Trim());
                            }
                        }
                    }
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;
                message.Priority = MailPriority.High;
                var smtpClient = new SmtpClient();
                smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSl"]);
                smtpClient.Send(message);
                isSuccess = true;
            }
            catch (Exception ex) {
                isSuccess = false;
            }
            finally
            {
                // Clean up.
                message.Dispose();
            }
            return isSuccess;

        }

        #endregion


        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        public static bool SendEmail(string subject, string body, string from, string to, bool isLinked)
        {
            return SendEmail(subject, body, new MailAddress(from), new MailAddress(to),
               new List<String>(), new List<String>(), true, true);
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        /// <param name="bcc">BCC</param>
        /// <param name="cc">CC</param>
        /// <param name="emailAccount">Email account to use</param>
        public static bool SendEmail(string subject, string body,
            MailAddress from, MailAddress to, List<string> bcc, List<string> cc, bool isHtml, bool isLinked)
        {

            bool isSuccess = false;
            var message = new MailMessage();
            try
            {


                if (from != null)
                    message.From = from;

                message.To.Add(to);

                if (ConfigurationManager.AppSettings["bccEmail"] != "")
                    message.Bcc.Add(ConfigurationManager.AppSettings["bccEmail"]);

                if (null != bcc)
                    foreach (string address in bcc)
                    {
                        if (address != null)
                        {
                            if (!String.IsNullOrEmpty(address.Trim()))
                            {
                                message.Bcc.Add(address.Trim());
                            }
                        }
                    }
                if (null != cc)
                    foreach (string address in cc)
                    {
                        if (address != null)
                        {
                            if (!String.IsNullOrEmpty(address.Trim()))
                            {
                                message.CC.Add(address.Trim());
                            }
                        }
                    }
                message.Priority = MailPriority.High;
                message.Subject = subject;
                message.IsBodyHtml = isHtml;
                //message.Body = body;

                //string logoUri = ConfigurationManager.AppSettings["ipAndPortForUrlPath"] + "/images/appicon.png";
                //string replyMsg = "<br /><img src='" + logoUri + "' width='72px' height='72px' /><br /><br /><br /><div style='color:Gray; font-size:smaller'>This is a system-generated email; please do not reply.</div>";

                string mailContent = body;

                AlternateView htmlview = default(AlternateView);
                htmlview = AlternateView.CreateAlternateViewFromString(mailContent, null, "text/html");


                //LinkedResource imageResourceEs = new LinkedResource(imageFiename, "image/png");
                //imageResourceEs.ContentId = "image1";
                //imageResourceEs.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                //htmlview.LinkedResources.Add(imageResourceEs);

                message.AlternateViews.Add(htmlview);
                var smtpClient = new SmtpClient();
                smtpClient.EnableSsl = PPI.Core.Web.Properties.Settings.Default.SMTPSSL;
                smtpClient.Send(message);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                //Log the exception                   
            }
            finally
            {
                // Clean up.
                message.Dispose();
            }
            return isSuccess;
        }
    }
}
