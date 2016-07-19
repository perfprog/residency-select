using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class SiteMVPIViewModel
    {
        public int programId { get; set; }
        public int ProgramSiteId { get; set; }
        public Site Site { get; set; }        
        public ProgramSiteHoganMVPI HoganMVPI { get; set; }

    }    
}