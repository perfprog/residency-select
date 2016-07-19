using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class ReportTextModel
    {
        public PracticeCategory Category { get; set; }

        public PracticeScaleReport ReportScale { get; set; }

        public TranslationText Text;

        public IEnumerable<TranslationText> AlternativeText { get; set; }
        
       
    }
    public class TranslationText
    {
        public string EnglishText;
        public PracticeText Text;
        public IEnumerable<TranslationText> ChildText { get; set; }
    }
}