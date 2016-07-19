using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class OESReviewViewModel
    {
        public int Id { get; set; }
        public int OESSetupID { get; set; }
        public OESSetup Setup { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ReviewDate { get; set; }
        public string CompositeNeedByDate { get; set; }
        public string JETNeedByDate { get; set; }
        public string ReportsSelected { get; set; }
    }
}