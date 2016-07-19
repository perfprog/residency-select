using System.Data.Entity;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;
using System.Linq.Expressions;
namespace PPI.Core.Web.Models
{
    public class PersonProfileViewModel
    {
        public string Hogan_Id { get; set; }
        public Person Person { get; set; }
        public List<ProgramEventModel> ProgramEvents { get; set; }
        public string FullName { get; set; }
        public string HPI { get; set; }
        public string HDS { get; set; }
        public string MVPI { get; set; }
        public string AsOfDate { get; set; }
        public string HoganProcess { get; set; }
        public string J3PProcess { get; set; }

        public List<PersonEmail> PersonEmails { get; set; }

        public List<int> ReportDataAvailible { get; set; }

    }

    public class ProgramEventModel
    {
        public string ProgramName { get; set; }
        public int ProgramId { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }    
}