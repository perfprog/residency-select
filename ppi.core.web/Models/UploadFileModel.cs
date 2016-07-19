using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class UploadFileModel
    {
        public string Name { get; set; }
        public int Length { get; set; }

        public int SuccessfullCount { get; set; }
        public int FailedCount { get; set; }
    }
}