using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class HoganLoadCheckViewModel
    {
        public int ProgramSiteEventId { get; set; }
        public List<FileRecordCount> Files { get; set; } 
    }
    public class FileRecordCount
    {
        public int recordcount { get; set; }                
        public FileInfo File { get; set; }
    }
}