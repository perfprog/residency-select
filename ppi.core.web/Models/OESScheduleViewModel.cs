using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class OESScheduleViewModel
    {
        public int Id { get; set; }
        public int OESSetupId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ReviewDate { get; set; }
        public string CompositeNeedByDate { get; set; }
        public string JETNeedByDate { get; set; }

        public bool JetRequired { get; set; }
        public bool CompositeRequired { get; set; }
    }
}