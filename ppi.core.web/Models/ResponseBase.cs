using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class ResponseBase
    {
        public int Code { get; set; }
        public string ResponseMsg { get; set; }
    }
}