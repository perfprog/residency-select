using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPI.Core.Web.Models.Base;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;
using PPI.Core.Domain.Concrete;

namespace PPI.Core.Web.Infrastructure
{
    public class PracticeReportsViewModel : ReportViewModel
    {
        
        private int Language;
        private int Report;
        private ISpecialRepository<UserPracticeText> UserPracticeText;

        public string HoganID  {get;set;}

        public string ReportFor { get; set; }     
        public PracticeReportsViewModel(ISpecialRepository<UserPracticeText> userPracticeText, string hoganId, int language, int report)
        {
            HoganID = hoganId;
            Language = language;
            Report = report;
            UserPracticeText = userPracticeText;
        }
        public IEnumerable<UserPracticeText> GetUserPracticeText()
        {
            return UserPracticeText.GetUserPracticeText(HoganID, Language, Report);
        }
    }
}