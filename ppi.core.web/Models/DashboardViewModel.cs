using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class DashboardViewModel
    {
        public ValueRatio HPI { get; set; }
        public ValueRatio HDS { get; set; }
        public ValueRatio MVPI { get; set; }

        public ValueRatio AssessmentComplete { get; set; }
        public ValueRatio AssessmentNotComplete { get; set; }

        public int AssessmentsToday { get; set; }
        public int InvitationsTotal { get; set; }
        public int RemindersTotal { get; set; }
        public int? eventId { get; set; }

        public string EventName { get; set; }
        public DateTime AsOfDate { get; set; }

    }
    public class ValueRatio
    {
        public int NumberCompleted { get; set; }
        public int TotalNumber { get; set; }
        public double PercentComplete { get; set; }
        public DateTime AsOfDate { get; set; }
        
    }
}
