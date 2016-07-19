using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PPI.Core.Web.Models
{
    using PPI.Core.Domain.Entities;
    public class BillingViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<PersonPracticeReport> PracticeReports { get; set; }
    }
}