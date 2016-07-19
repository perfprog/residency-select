using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class GetReportsPostViewModel
    {
        public string[] SelectedUsers { get; set; }
        public string[] SelectedReports { get; set; }
        public string[] SingleEmail { get; set; }
        public int EventId { get; set; }
        public int ProgramSiteId { get; set; }
    }
}