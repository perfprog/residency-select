using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class PersonLoadViewModel
    {
        
        public int SelectedProgramSiteEventId { get; set; }
        public List<EventHoganCodes> EventHoganCodes { get; set; }

    }
    public class EventHoganCodes 
    {
        public int ProgramSiteEventId { get; set; }
        public int HoganCountCodesAvailible { get; set; }

    }
}