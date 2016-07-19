using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace PPI.Core.Web.Models
{
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;
    public class BillingReportViewModel
    {
        public IEnumerable<ReportsRun> ReportsSelected { get; set; }
    }

    public class ReportsRun
    {
        public PracticeReport Report { get; set; }

        public IEnumerable<Person> Participants { get; set; }
    }
}
