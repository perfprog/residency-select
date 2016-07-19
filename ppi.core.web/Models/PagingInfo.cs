using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class PagingInfo
    {
        public PagingInfo()
        {                        
            FirstPage = 1;         
            NavSize = 5;
        }
        public int? NextPage { get; set; }
        public int? PrevPage { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }

        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int NavSize { get; set; }
    }
}