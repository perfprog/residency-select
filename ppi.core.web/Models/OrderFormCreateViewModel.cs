using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class OrderFormCreateViewModel
    {
        public OrderForm OrderForm { get; set; }
         public string[] PracticeReportsIds { get; set; }
    }
}