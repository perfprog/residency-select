using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class PersonProgramSites
    {
        public int ProgramId { get; set; }
        public string ProgramName {get;set;}
        public IEnumerable<SiteReports> SiteReports { get; set; }
        
    }
    public class SiteReports
    {
        public Site Site { get; set; }
        public int ProgramSiteId { get; set; }
        public IEnumerable<ProgramPracticeReport> AvailibleReports { get; set; }
    }
}