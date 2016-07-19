using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class ResponseEmailModel
    {
        public string returnMessage { get; private set; }
        public EmailStatus EmailStatus { get; set; }
    }
}