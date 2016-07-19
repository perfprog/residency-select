using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class ProgramSiteViewModel
    {

        public int ProgramID { get; set; }

        public string ProgramName { get; set; }
        public IEnumerable<ProgramSite> ProgramSites { get; set; }
        public int SelectedOrganisationID { get; set; }
        public int SelectedDepartmentID { get; set; }

    }    
}