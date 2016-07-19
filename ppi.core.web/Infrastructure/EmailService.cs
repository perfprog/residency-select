using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Configuration;

namespace PPI.Core.Web.Infrastructure
{
    using PPI.Core.Domain.Entities;
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Web.Models;
    using SendGrid;
    using Ninject;
    using Ninject.Parameters;
    using Ninject.Syntax;
    using System.Net;
    public class EmailService
    {
        [Log]
        public void SendEmailAsync(List<EmailModel> emails, bool cloud, IUnitOfWork UnitWork)
        {

            foreach (var item in emails)
            {
                //  var client = new SmtpClient(PPI.Core.Web.Properties.Settings.Default.SMTPURI, int.Parse(PPI.Core.Web.Properties.Settings.Default.SMTPPORT));
                //  client.UseDefaultCredentials = PPI.Core.Web.Properties.Settings.Default.SMTPUseDefaultCreditals;
                //  client.EnableSsl = PPI.Core.Web.Properties.Settings.Default.SMTPSSL;
                //  client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //  client.Credentials = new System.Net.NetworkCredential(PPI.Core.Web.Properties.Settings.Default.SMTPUSER, PPI.Core.Web.Properties.Settings.Default.SMTPPASSWORD);

                var Credentials = new NetworkCredential(
                    PPI.Core.Web.Properties.Settings.Default.SMTPUSER,
                    PPI.Core.Web.Properties.Settings.Default.SMTPPASSWORD
                    );

                var transportWeb = new SendGrid.Web(Credentials);

                var Mail = new SendGrid.SendGridMessage();


                Mail.AddTo(isValidEmail(item.to).Address);
                Mail.From = (isValidEmail(item.from));
                if (item.cc != null && item.cc != "")
                    Mail.AddBcc(isValidEmail(item.cc));


                if (item.bcc != null && item.bcc != "")
                    Mail.AddBcc(isValidEmail(item.bcc));

                if (ConfigurationManager.AppSettings["bccEmail"] != "")
                    Mail.AddBcc(isValidEmail(ConfigurationManager.AppSettings["bccEmail"]));



                Mail.Subject = item.subject != null ? item.subject : PPI.Core.Web.Properties.Settings.Default.SMTPUseDefaultSubject;
                Mail.Html = string.IsNullOrEmpty(item.body) ? PPI.Core.Web.Properties.Settings.Default.SMTPUseDefaultSubject : item.body;
                if (item.AttachmentPath != "" && item.AttachmentPath != string.Empty && item.AttachmentPath != null)
                {
                    Mail.AddAttachment(item.AttachmentPath);
                }


                var personEmail = UnitWork.IPersonEmailRepository.AsQueryable().FirstOrDefault(m => m.Id == item.personEmailId);
                try
                {
                    if (transportWeb != null)
                    {
                        transportWeb.Deliver(Mail);
                        personEmail.EmailStatusId = 1; // Sent                                
                        item.emailStatusId = 1;
                    }
                }
                catch (Exception ex)
                {
                    string error = "";
                    if (ex.GetType() == typeof(Exceptions.InvalidApiRequestException))
                    {
                        HttpStatusCode code = ((Exceptions.InvalidApiRequestException)ex).ResponseStatusCode;
                        string strCode = ((byte)code).ToString();
                        error += "\nSTATUS CODE: " + strCode + " " + code.ToString();
                        error += "\n\nERRORS: ";
                        foreach (string err in ((Exceptions.InvalidApiRequestException)ex).Errors)
                        {
                            error += "\n" + err;
                        }
                    }
                    else
                    {
                        error += "\n" + ex.Message;
                    }


                    personEmail.EmailStatusId = 2; // failed
                    personEmail.ErrorMessage = error;
                    item.emailStatusId = 2;
                }

                if (item.scheduledEmailPersonId != null)
                {
                    var scheduledEmailItem = UnitWork.IScheduledEmailPersonRepository.SingleOrDefault(m => m.Id == item.scheduledEmailPersonId);
                    scheduledEmailItem.CompletedDate = DateTime.Now;
                    UnitWork.IScheduledEmailPersonRepository.Update(scheduledEmailItem);
                }

                UnitWork.IPersonEmailRepository.Update(personEmail);
                personEmail.SentDate = DateTime.Now;
                UnitWork.Commit();
            }

        }


        [Log]
        public bool SendEmailAsync(List<EmailModel> emails, bool cloud)
        {
            bool isSend = false;
            foreach (var item in emails)
            {
                //  var client = new SmtpClient(PPI.Core.Web.Properties.Settings.Default.SMTPURI, int.Parse(PPI.Core.Web.Properties.Settings.Default.SMTPPORT));
                //  client.UseDefaultCredentials = PPI.Core.Web.Properties.Settings.Default.SMTPUseDefaultCreditals;
                //  client.EnableSsl = PPI.Core.Web.Properties.Settings.Default.SMTPSSL;
                //  client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //  client.Credentials = new System.Net.NetworkCredential(PPI.Core.Web.Properties.Settings.Default.SMTPUSER, PPI.Core.Web.Properties.Settings.Default.SMTPPASSWORD);

                var Credentials = new NetworkCredential(
                    PPI.Core.Web.Properties.Settings.Default.SMTPUSER,
                    PPI.Core.Web.Properties.Settings.Default.SMTPPASSWORD
                    );

                var transportWeb = new SendGrid.Web(Credentials);

                var Mail = new SendGrid.SendGridMessage();


                Mail.AddTo(isValidEmail(item.to).Address);
                Mail.From = (isValidEmail(item.from));
                if (item.cc != null && item.cc != "")
                    Mail.AddBcc(isValidEmail(item.cc));


                if (item.bcc != null && item.bcc != "")
                    Mail.AddBcc(isValidEmail(item.bcc));

                if (ConfigurationManager.AppSettings["bccEmail"] != "")
                    Mail.AddBcc(isValidEmail(ConfigurationManager.AppSettings["bccEmail"]));



                Mail.Subject = item.subject != null ? item.subject : PPI.Core.Web.Properties.Settings.Default.SMTPUseDefaultSubject;
                Mail.Html = string.IsNullOrEmpty(item.body) ? PPI.Core.Web.Properties.Settings.Default.SMTPUseDefaultSubject : item.body;
                if (item.AttachmentPath != "" && item.AttachmentPath != string.Empty && item.AttachmentPath != null)
                {
                    Mail.AddAttachment(item.AttachmentPath);
                }

                try
                {
                    if (transportWeb != null)
                    {
                        transportWeb.Deliver(Mail);
                        item.emailStatusId = 1;
                        isSend = true;
                    }
                }
                catch (Exception ex)
                {
                    string error = "";
                    if (ex.GetType() == typeof(Exceptions.InvalidApiRequestException))
                    {
                        HttpStatusCode code = ((Exceptions.InvalidApiRequestException)ex).ResponseStatusCode;
                        string strCode = ((byte)code).ToString();
                        error += "\nSTATUS CODE: " + strCode + " " + code.ToString();
                        error += "\n\nERRORS: ";
                        foreach (string err in ((Exceptions.InvalidApiRequestException)ex).Errors)
                        {
                            error += "\n" + err;
                        }
                    }
                    else
                    {
                        error += "\n" + ex.Message;
                    }

                    item.emailStatusId = 2;
                }
            }
            return isSend;

        }





        [Log]
        private System.Net.Mail.MailAddress isValidEmail(string address)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(address);
                return addr;
            }
            catch (Exception)
            {
                return new MailAddress(PPI.Core.Web.Properties.Settings.Default.InValidEmailAddress);
            }
        }

        [Log]
        static public bool isValidEmailAlt(string address)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(address);
                return (addr != null ? true : false);
            }
            catch (Exception)
            {
                return (false);
            }
        }
    }
}
