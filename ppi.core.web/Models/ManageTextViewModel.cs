using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class ManageTextViewModel
    {
        public IEnumerable<Program> Programs { get; set; }
        public int ProgramId;
        public IEnumerable<PracticeReport> Reports { get; set; }
        public int ReportId;
        public IEnumerable<HoganField> Scales { get; set; }
        public int ScaleId;
        public IEnumerable<Culture> Cultures { get; set; }
        public int CultureId;
        public List<string> AvailibleCategories { get; set; }
        public IEnumerable<ReportTextModel> ReportText { get; set; }
    }
}