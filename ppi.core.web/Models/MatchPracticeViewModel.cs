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
    public class PracticeMatchReportsViewModel : ReportViewModel
    {

        private int Language;
        private int Report;
        private ISpecialRepository<UserPracticeCategoryText> UserPracticeCategoryText;
        private int ProgramId;
        public string HoganID { get; set; }

        public string ReportFor { get; set; }
        public PracticeMatchReportsViewModel(ISpecialRepository<UserPracticeCategoryText> userPracticeCategoryText, string hoganId, int language, int report, int programId)
        {
            HoganID = hoganId;
            Language = language;
            Report = report;
            ProgramId = programId;
            UserPracticeCategoryText = userPracticeCategoryText;
        }
        public IEnumerable<UserPracticeCategoryText> GetUserPracticeCategoryText()
        {
            return UserPracticeCategoryText.GetUserPracticeCategoryText(HoganID, Language, Report, ProgramId);

        }
    }
}