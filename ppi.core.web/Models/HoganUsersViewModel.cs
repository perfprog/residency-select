using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class HoganUsersViewModel
    {
        public List<HoganProgramCounts> HoganPrograms { get; set; }
    }
    public class HoganProgramCounts
    {
        public string ProgramName { get; set; }
        public int Count { get; set; }
    }
}