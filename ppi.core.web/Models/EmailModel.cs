using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class EmailModel
    {
        public int personEmailId { get; set; }
        public string to { get;  set; }
        public string from { get;  set; }
        public string cc { get;  set; }
        public string bcc { get;  set; }
        public string body { get;  set; }
        public string subject { get;  set; }

        public string AttachmentPath { get; set; }
        public int emailStatusId { get; set; }

        public int? scheduledEmailPersonId { get; set; }        
    }
}