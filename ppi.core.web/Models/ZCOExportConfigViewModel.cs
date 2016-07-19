using PPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class ZCOCheckBoxItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }

    public class ZCOExportConfigViewModel
    {        
        public IEnumerable<ZCOCheckBoxItem> ExportCols { get; set; }

        public int? NumPersonsExported { get; set; }        
    }

}