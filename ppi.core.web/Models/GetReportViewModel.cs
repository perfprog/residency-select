using PPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class GetReportViewModel
    {
        public IEnumerable<Person> Participants { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<EventPracticeReport> Reports { get; set; }
        public int? EventId { get; set; }
        public List<PeopleAvailibleReports> PeopleAvailibleReports { get; set; }
    }
    public class PeopleAvailibleReports
    {
        public int PersonId { get; set; }
        public List<int> ReportDataAvailible { get; set; }
    }

}