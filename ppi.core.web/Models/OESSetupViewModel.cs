using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class OESSetupViewModel
    {
        public string UserName { get; set; }
        public OESSetup Setup { get; set; }
        public List<CheckBoxModel> AvailibleReports { get; set; }
        public string[] PracticeReportsIds { get; set; }
        public Site Site { get; set; }
    }
}