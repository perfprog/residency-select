using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class HoganViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        
        public IGenericRepository<Manual_Hogan_Import> Manual_Hogan_ImportRepository { get; set; }

        public IEnumerable<Manual_Hogan_Import> Manual_Hogan_Imports { get; set; }


    
    }
}