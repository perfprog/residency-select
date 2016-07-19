using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Core.Web.Models
{
    public class PracticeReportModel
    {
        public PracticeReportModel() { }
        public byte[] Logo {get;set;}
        public string LogoMimeType {get;set;}
        public byte[] Background{get;set;}
        public string BackgroundMimeType {get;set;}
        public string Color {get;set;}
        public string Title {get;set;}
        public string Introduction { get; set; }
        public string IntroductionTwo { get; set; }
        public string IntroductionThree { get; set; }
        public string HoganId {get;set;}
        public string ReportFor {get;set;}

    
    }
}