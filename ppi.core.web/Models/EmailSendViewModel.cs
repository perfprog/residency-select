using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class EmailSendViewModel
    {
        public int? EventId { get; set; }                       
        public IEnumerable<Mailing> Mailer { get; set; }
        public EmailOps Ops { get; set; }
    }

    public class Mailing
    {
        public Email Email { get; set; }
        public IEnumerable<Person> Participants { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public bool active { get; set; }
    }

    public class EmailOps
    {
        public bool NothingFlag { get; set; }
        public bool ScheduleFlag { get; set; }
        public bool RemoveFlag { get; set; }
        public bool EmailStatsFlag { get; set; }
        public int EmailsSent { get; set; }
        public int EmailsFailed { get; set; }
    }
}