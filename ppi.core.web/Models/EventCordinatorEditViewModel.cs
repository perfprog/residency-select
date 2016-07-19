using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class EventCordinatorEditViewModel
    {
        public string DefaultEmail { get; set; }
        public Event Event { get; set; }
        public string datepickerDateRange { get; set; }
    }
}