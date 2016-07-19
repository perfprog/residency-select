using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class PersonSiteProgramViewModel
    {
        public IEnumerable<Site> Sites { get; set; }
        public int PersonId { get; set; }
        public string Hogan_Id { get; set; }
        public int ProgramId { get; set; }
        public bool HasFitReport { get; set; }
        public int ProgramHostpitalId { get; set; }
        public string ProgramName { get; set; }
    }
}