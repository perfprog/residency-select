using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PPI.Core.Web.Models
{
    public class AssessmentViewModel
    {
        [Key]
        public int ID { get; set; }
        public PagingInfo PagingInfo { get; set; }
        
        public IEnumerable<AssessmentQuestion> AssessmentQuestions { get; set; }
        public IEnumerable<AssessmentQuestionOption> AssessmentQuestionOptions { get; set; }
    }
}